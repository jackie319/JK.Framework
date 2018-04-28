using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions.Email
{
    public  class Email
    {
        /// <summary>
        /// 发件人地址
        /// </summary>
        public string FromEmail { get; set; }
        /// <summary>
        ///  	发信人昵称
        /// </summary>
        public string FromAlias { get; set; }
        /// <summary>
        /// 授权码
        /// </summary>
        public string Accesskey { get; set; }
        /// <summary>
        /// SMTP服务器地址 .smtp.qq.com
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// SMTP端口 QQ邮箱填写587 
        /// </summary>
        public int Port { get; set; }

        public Email(string fromEmail,string fromAlias,string accesskey,string host,int port)
        {
            FromEmail = fromEmail;
            FromAlias = fromAlias;
            Accesskey = accesskey;
            Host = host;
            Port = port;
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  bool SendEmail(EmailSendModel model)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(model.ToEmail);//收件人地址 
            if (!string.IsNullOrEmpty(model.CCEmail))
            {
                msg.CC.Add(model.CCEmail);//抄送人地址 
            }
        
            msg.From = new MailAddress(FromEmail,FromAlias);//发件人邮箱，名称 

            msg.Subject = model.Subject;//邮件标题 
            msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8 

            msg.Body = model.HtmlBody;//邮件内容 
            msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8 

            SmtpClient client = new SmtpClient();

            client.Host = Host;//SMTP服务器地址 
            client.Port = Port;//SMTP端口，QQ邮箱填写587 

            client.EnableSsl = true;//启用SSL加密 
                                    //发件人邮箱账号，授权码(注意此处，是授权码你需要到qq邮箱里点设置开启Smtp服务，然后会提示你第三方登录时密码处填写授权码)
            client.Credentials = new System.Net.NetworkCredential(FromEmail,Accesskey);

            try
            {
                client.Send(msg);//发送邮件
            }
            catch (Exception ex)
            {
                throw new Exception("发送邮件出现错误:" + ex.Message);
            }
            return true;
        }
    }

    public class EmailSendModel
    {
        /// <summary>
        /// 收件人地址
        /// </summary>
        public string ToEmail { get; set; }
        /// <summary>
        /// 抄送人地址
        /// </summary>
        public string CCEmail { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件正文
        /// </summary>
        public string HtmlBody { get; set; }
   
    }
}
