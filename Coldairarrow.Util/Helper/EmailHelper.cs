using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Coldairarrow.Util.Helper
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Name">发件人名字</param>
        /// <param name="receive">接收邮箱</param>
        /// <param name="sender">发送邮箱</param>
        /// <param name="password">邮箱密码</param>
        /// <param name="host">邮箱主机</param>
        /// <param name="port">邮箱端口</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <returns></returns>
        public  bool SendMail(string Name, string receive, string sender, string password, string host, int port, string subject, string body)
        {
            try
            {
                // MimeMessage代表一封电子邮件的对象
                var message = new MimeMessage();
                //添加发件人地址 Name 发件人名字 sender 发件人邮箱
                message.From.Add(new MailboxAddress(Name, sender));
                // 添加收件人地址
                message.To.Add(new MailboxAddress("", receive));
                // 设置邮件主题信息
                message.Subject = subject;
                // 设置邮件内容
                var bodyBuilder = new BodyBuilder() { HtmlBody = body };
                message.Body = bodyBuilder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    //接受所有SSL证书（以防服务器支持STARTTLS）
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    // 由于我们没有OAuth2令牌，请禁用 
                    //XOAUTH2身份验证机制。
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.CheckCertificateRevocation = false;
                    //client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                    client.Connect(host, port, MailKit.Security.SecureSocketOptions.Auto);
                    //仅当SMTP服务器需要身份验证时才需要
                    client.Authenticate(sender, password);
                    client.SendAsync(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
