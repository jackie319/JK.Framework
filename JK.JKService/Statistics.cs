using JK.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace JK.JKService
{
    public class Statistics
    {
        private static Timer _SmsTimer;
        private P2bService Server { get; set; }
        public Statistics(P2bService server)
        {
            Server = server;
        }

        public void BeginStatistics()
        {
            LogTool.DailyRecord("统计计时器开始计时", "begin:" + DateTime.Now.ToString("yyyyMMddHHmmss"));
            _SmsTimer = new Timer(1000 * 60 * 1);//TODO:先硬编码
            _SmsTimer.Elapsed += ElapsedEventHandler;
            _SmsTimer.Start();
        }
        public void ElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            _SmsTimer.Stop();
            //执行统计的时间区间
            if (DateTime.Now.Hour > 1 && DateTime.Now.Hour < 23)
            {
                try
                {
                    LogTool.DailyRecord("开始统计", "begin:" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                    //具体业务
                    //Server.Earning.UserEarningStatistics();
                    LogTool.DailyRecord("统计结束", "begin:" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                }

                catch (Exception ex)
                {

                    LogTool.DailyRecord("捕获统计异常", "为：" + ex.Message + ";" + ex.StackTrace);
                }
                //计时器不能停止
                finally
                {
                    _SmsTimer.Start();
                }
            }
            else
            {
                _SmsTimer.Start();
            }

        }
    }
}
