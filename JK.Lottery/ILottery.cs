using JK.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Lottery
{
    interface ILottery
    {
        void CreatedActivity(LotteryActivity entity, IList<LotteryPrize> prizeList);
        void UpdateActivity(LotteryActivity entity, IList<LotteryPrize> prizeList);
        IList<LotteryActivity> GetList(string activityTitle, int skip, int take, out int total);
        LotteryActivity FindPrizeActivity(Guid activityGuid);
        void DeleteActivity(Guid activityGuid);
        LotteryJackpot GetJackPot();
        LotteryPrize Draw(Guid activityGuid, Guid userGuid);
        IList<MyLotteryV> GetMyLotteryHistory(Guid userGuid, int skip, int take, out int total);
    }
}
