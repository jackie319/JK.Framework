using JK.Data.Model;
using JK.Framework.Core.Data;
using JK.Framework.Extensions.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Lottery
{
    public class LotteryImpl : ILottery
    {
        private IRepository<LotteryActivity> _PrizeActivityRepository;
        private IRepository<LotteryPrize> _PrizeRepository;
        private IRepository<LotteryHistory> _LotteryHistoryRepository;
        private IRepository<LotteryJackpot> _LotteryJackpotRepository;
        private IRepository<UserAccount> _UserAccountRepository;
        private IRepository<MyLotteryV> _MyLotteryRepository;
        IRepository<FXJLSetting> _FXJLSettingRepository;

        public LotteryImpl(IRepository<LotteryActivity> prizeActivityRepository, IRepository<LotteryPrize> prizeRepository, IRepository<LotteryHistory> lotteryHistoryRepository,
            IRepository<LotteryJackpot> lotteryJackpotRepository, IRepository<UserAccount> userAccountRepository,
            IRepository<MyLotteryV> MyLotteryRepository, IRepository<FXJLSetting> FXJLSettingRepository)
        {
            _PrizeActivityRepository = prizeActivityRepository;
            _PrizeRepository = prizeRepository;
            _LotteryHistoryRepository = lotteryHistoryRepository;
            _LotteryJackpotRepository = lotteryJackpotRepository;
            _UserAccountRepository = userAccountRepository;
            _MyLotteryRepository = MyLotteryRepository;
            _FXJLSettingRepository = FXJLSettingRepository;
        }

        public void CreatedActivity(LotteryActivity entity, IList<LotteryPrize> prizeList)
        {
            var activityGuid = Guid.NewGuid();
            entity.Guid = activityGuid;
            entity.TimeCreated = DateTime.Now;
            entity.IsDeleted = false;
            _PrizeActivityRepository.Insert(entity);
            foreach (var item in prizeList)
            {
                item.PrizeActivityGuid = activityGuid;
                CreatedPrize(item);
            }

        }

        public void UpdateActivity(LotteryActivity entity, IList<LotteryPrize> prizeList)
        {
            var oldEntity = _PrizeActivityRepository.Table.FirstOrDefault(q => q.Guid == entity.Guid & !q.IsDeleted);
            if (oldEntity == null) throw new ArgumentException("找不到该活动");
            oldEntity.Type = entity.Type;
            oldEntity.Title = entity.Title;
            oldEntity.BaseNumber = entity.BaseNumber;
            oldEntity.DefaultPic = entity.DefaultPic;
            oldEntity.Detail = entity.Detail;
            oldEntity.BeginTime = entity.BeginTime;
            oldEntity.EndTime = entity.EndTime;
            _PrizeActivityRepository.Update(oldEntity);

            DeletePrize(entity.Guid);

            foreach (var item in prizeList)
            {
                item.PrizeActivityGuid = entity.Guid;
                CreatedPrize(item);
            }
        }

        public IList<LotteryActivity> GetList(string activityTitle, int skip, int take, out int total)
        {
            var query = _PrizeActivityRepository.Table.Where(q => !q.IsDeleted);
            if (string.IsNullOrEmpty(activityTitle))
            {
                query = query.Where(q => q.Title.Contains(activityTitle));
            }
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

        public LotteryActivity FindPrizeActivity(Guid activityGuid)
        {
            var entity = _PrizeActivityRepository.Table.FirstOrDefault(q => q.Guid == activityGuid);
            if (entity == null) throw new ArgumentException("找不到该活动");
            return entity;
        }

        public void DeleteActivity(Guid activityGuid)
        {
            var entity = _PrizeActivityRepository.Table.FirstOrDefault(q => q.Guid == activityGuid);
            if (entity == null) throw new ArgumentException("找不到该活动");
            entity.IsDeleted = true;
            _PrizeActivityRepository.Update(entity);
        }

        public LotteryPrize FindPrize(Guid prizeGuid)
        {
            var entity = _PrizeRepository.Table.FirstOrDefault(q => q.Guid == prizeGuid);
            if (entity == null) throw new ArgumentException("找不到该奖品");
            return entity;
        }

        public void UpdatePrizeNum(Guid prizeGuid)
        {
            var entity = _PrizeRepository.Table.FirstOrDefault(q => q.Guid == prizeGuid);
            if (entity == null) throw new ArgumentException("找不到该奖品");
            entity.TotalNum -= 1;
            _PrizeRepository.Update(entity);
        }

        private void CreatedPrize(LotteryPrize entity)
        {
            entity.Guid = Guid.NewGuid();
            entity.TimeCreated = DateTime.Now;
            entity.IsDeleted = false;
            _PrizeRepository.Insert(entity);
        }

        private void DeletePrize(Guid activityGuid)
        {
            var list = _PrizeRepository.Table.Where(q => q.PrizeActivityGuid == activityGuid).ToList();
            foreach (var item in list)
            {
                item.IsDeleted = true;
                _PrizeRepository.Update(item);
            }

        }


        public LotteryJackpot GetJackPot()
        {
            return _LotteryJackpotRepository.Table.FirstOrDefault();
        }
        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="activityGuid"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public LotteryPrize Draw(Guid activityGuid, Guid userGuid)
        {
            var userEntity = _UserAccountRepository.Table.FirstOrDefault(q => q.Guid == userGuid && !q.IsDeleted);
            if (userEntity == null) throw new ArgumentException("用户不存在");
            if (userEntity.LuckTotal < 1) throw new ArgumentException("抽奖机会不足");
            int money = 0;
            Boolean isWinner = false;
            LotteryPrize prize = new LotteryPrize();
            var prizeGuid = Guid.Empty;
            var activity = _PrizeActivityRepository.Table.FirstOrDefault(q => q.Guid == activityGuid && !q.IsDeleted);
            //剩余奖品数大于0
            var prizeList = _PrizeRepository.Where(q => q.PrizeActivityGuid == activityGuid && !q.IsDeleted && q.TotalNum > 0);
            int total = activity.BaseNumber;
            int prizeTotal = 0;//所有奖品中奖率总和
            IList<Guid> box = new List<Guid>();//奖品盒子

            foreach (var item in prizeList)
            {
                prizeTotal += item.WinningRate;

                if (item.WinningRate > 100000) throw new ArgumentException("建议重新配置抽奖参数");
                //中奖几率为几就往盒子里装几个奖品
                for (int i = 0; i < item.WinningRate; i++)
                {
                    box.Add(item.Guid);
                }
            }
            //prizeTotal应小于total 否则必中
            if (prizeTotal > total)
            {
                throw new ArgumentException("中奖几率不能大于活动总基数");
            }

            box.ToOutOfOrder();//乱序
                               //种子精确到百纳秒级别
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            int num = r.Next(1, total);
            if (num > prizeTotal)//未中奖
            {
                prizeGuid = Guid.Empty;
            }
            else
            {
                prizeGuid = box[num - 1];
                //奖品数减1
                UpdatePrizeNum(prizeGuid);
            }
            if (prizeGuid != Guid.Empty)
            {
                foreach (var item in prizeList)
                {
                    if (item.Guid == prizeGuid)
                    {
                        prize = item;
                        money = item.Money;
                        isWinner = true;
                        break;
                    }
                }
            }

            //抽奖记录
            LotteryHistory history = new LotteryHistory();
            history.PrizeActivityGuid = activityGuid;
            history.UserGuid = userGuid;
            history.IsWenner = isWinner;
            history.PrizeGuid = prizeGuid;
            CreatedLotteryHistory(history);
            ChangeJackPot(isWinner, money, userGuid);
            return prize;
        }

        /// <summary>
        /// 分配奖金，调节奖池，减去抽奖机会
        /// 中奖相应奖品数减1
        /// </summary>
        /// <param name="isWenner"></param>
        /// <param name="money"></param>
        /// <param name="userGuid"></param>
        private void ChangeJackPot(Boolean isWenner, int money, Guid userGuid)
        {
            var setting = _FXJLSettingRepository.Table.FirstOrDefault();
            int once = setting.MoneyToLuckTime;//一次抽奖机会对应的金额
            if (isWenner)
            {
                var entity = _LotteryJackpotRepository.Table.FirstOrDefault();
                entity.Total += once;
                entity.Total -= money;
                if (entity.Total < 0) entity.Total = 0;
                entity.TimeUpdate = DateTime.Now;
                _LotteryJackpotRepository.Update(entity);

                var userEntity = _UserAccountRepository.Table.FirstOrDefault(q => q.Guid == userGuid);
                userEntity.Money += money;
                userEntity.LuckTotal -= 1;
                _UserAccountRepository.Update(userEntity);
            }
            else
            {
                var entity = _LotteryJackpotRepository.Table.FirstOrDefault();
                entity.Total += once;
                entity.TimeUpdate = DateTime.Now;
                _LotteryJackpotRepository.Update(entity);

                var userEntity = _UserAccountRepository.Table.FirstOrDefault(q => q.Guid == userGuid);
                userEntity.LuckTotal -= 1;
                if (userEntity.LuckTotal < 0) userEntity.LuckTotal = 0;
                _UserAccountRepository.Update(userEntity);
            }
        }
        /// <summary>
        /// 抽奖记录
        /// </summary>
        /// <param name="entity"></param>
        private void CreatedLotteryHistory(LotteryHistory entity)
        {
            entity.Guid = Guid.NewGuid();
            entity.TimeCreated = DateTime.Now;
            _LotteryHistoryRepository.Insert(entity);
        }
        /// <summary>
        /// 抽奖历史
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<MyLotteryV> GetMyLotteryHistory(Guid userGuid, int skip, int take, out int total)
        {
            var query = _MyLotteryRepository.Table.Where(q => q.UserGuid == userGuid);
            total = query.Count();
            return query.OrderByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
        }

    }
}
