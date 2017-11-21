using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions.Draw
{
    /// <summary>
    /// 抽奖
    /// </summary>
   public class LotteryDraw
    {

        /// <summary>
        /// 活动本身没有中奖率，奖品才有中奖率
        /// 返回中奖奖品Guid（空为不中奖）
        /// 得到中奖奖品Guid后去数据库比对当前奖品有没有发放完。如果发放完则改为未中奖。
        /// 每次都是独立事件。没必要那么精确
        /// WinningRate 不能无限大，否则内存溢出。
        /// prizeTotal应小于total 否则必中
        /// </summary>
        /// <returns></returns>
        public Guid Draw()
        {
            int total = 10000;//抽奖活动总基数，动态读取
            int prizeTotal = 0;//所有奖品中奖率总和

            IList<Prize> prizeList = new List<Prize>();//动态读取
            IList<Guid> box = new List<Guid>();//奖品盒子
            foreach (var item in prizeList)
            {
                prizeTotal += item.WinningRate;

                if (item.WinningRate > 100000) throw new ArgumentException("建议重新配置抽奖参数");
                //中奖几率为几就往盒子里装几个奖品
                for (int i = 0; i < item.WinningRate; i++)
                {
                    box.Add(item.PrizeGuid);
                }
            }
            //prizeTotal应小于total 否则必中

            box.ToOutOfOrder();//乱序

            //种子精确到百纳秒级别
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            int num = r.Next(1, total);
            if (num > prizeTotal)//未中奖
            {
                return Guid.Empty;
            }
            else
            {
                return box[num - 1];
            }
        }

    }

    /// <summary>
    /// 奖品
    /// </summary>
    internal class Prize
    {
        public Guid PrizeGuid { get; set; }

        /// <summary>
        /// 中奖率（抽奖活动总基数分之n，及 WinningRate/total ）
        /// WinningRate 不能无限大，否则内存溢出。
        /// </summary>
        public int WinningRate { get; set; }
    }

    
}
