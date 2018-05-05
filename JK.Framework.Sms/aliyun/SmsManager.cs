using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Sms.aliyun
{

    public class SmsManager
    {
        private string AccessKeyId { get; set; }//你的accessKeyId，参考本文档步骤2
        private string AccessKeySecret { get; set; }//你的accessKeySecret，参考本文档步骤2

        private string Product = "Dysmsapi";//短信API产品名称（短信产品名固定，无需修改）
        private string Domain = "dysmsapi.aliyuncs.com";//短信API产品域名（接口地址固定，无需修改）
        public SmsManager(string accessKeyId,string accessKeySecret)
        {
            AccessKeyId = accessKeyId;
            AccessKeySecret = accessKeySecret;
        }
        public SendSmsResponse SendRegisteCode(string phoneNumbers,string signName,string templateCode) {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", AccessKeyId, AccessKeySecret);
            //IAcsClient client = new DefaultAcsClient(profile);
            // SingleSendSmsRequest request = new SingleSendSmsRequest();
            //初始化ascClient,暂时不支持多region（请勿修改）
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", Product, Domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式，发送国际/港澳台消息时，接收号码格式为00+国际区号+号码，如“0085200000000”
                request.PhoneNumbers = phoneNumbers;
                //必填:短信签名-可在短信控制台中找到
                request.SignName = signName;
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = templateCode;
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                //request.TemplateParam = "{\"name\":\"Tom\"， \"code\":\"123\"}";
                //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
               // request.OutId = "yourOutId";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                return sendSmsResponse;
                //状态码code-返回OK代表请求成功,其他错误码详见错误码列表
                //Message 	 	请求成功 	状态码的描述
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
