using MailKit.Security;
using MimeKit;
using Serilog;

namespace Backend.ThirdParty.Email;

public static class Mail163
{
    public static async Task<bool> SendEmail(string mailTo, string code)
    {
        try
        {
            var message = NewVerifyMail(mailTo, code);

            using var client = new MailKit.Net.Smtp.SmtpClient();

#if DEBUG
            await client.ConnectAsync(SmtpServer, SmtpPort, SecureSocketOptions.SslOnConnect);
#else
            await client.ConnectAsync(SmtpServer, SmtpPort, SecureSocketOptions.SslOnConnect);
#endif

            await client.AuthenticateAsync(MailAccount, AuthCode);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            Log.Information("mailTo{mailTo}Successfully", mailTo);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("mailTo{mailTo}Failed, ex:{@ex}", mailTo, ex);
            return false;
        }
    }


    /// <summary>
    /// 5分钟过期
    /// </summary>
    public const int ExpireMinutes = 5;

    #region Private

    /// <summary>
    /// POP3/SMTP服务授权密码
    /// </summary>
    private const string AuthCode = "...";

    /// <summary>
    /// 用户名与邮箱
    /// </summary>
    private const string MailAccount = "...";

    private const string MailName = "天童アリス";
    /// <summary>
    /// 邮件服务器主机与端口
    /// </summary>
    private const string SmtpServer = "...";
#if DEBUG
    //内网调试使用端口465作为目标->SslOnConnect
    private const int SmtpPort = 465;
#else
    //网易邮箱屏蔽了来自公网的目标25端口的访问，因此使用465->SslOnConnect/587->StartTls
    private const int SmtpPort = 465;
#endif

    /// <summary>
    /// 邮件主题与模版
    /// </summary>
    private const string MailSubject = "来自 chat.quetzalsidera.me 的验证邮件";

    private const string EmailTemplate = @"
<!DOCTYPE html><html lang=""zh-CN""><head><meta charset=""UTF-8""><meta name=""viewport"" content=""width=device-width,initial-scale=1.0""><title>爱丽丝 - 验证码邮件</title><style>*{{margin:0;padding:0;box-sizing:border-box;font-family:'Segoe UI','Microsoft YaHei',sans-serif}}:root{{--primary:#4A90E2;--secondary:#7ED0E8;--accent:#FFCC00;--light:#F5F9FF;--dark:#2C3E50;--gray:#7F8C8D;--white:#fff;--shadow:0 5px 15px rgba(0,0,0,.1)}}body{{background-color:var(--light);color:var(--dark);line-height:1.6;padding:20px}}.container{{max-width:600px;margin:0 auto;background:var(--white);border-radius:15px;overflow:hidden;box-shadow:var(--shadow)}}.header{{background:linear-gradient(135deg,#6a11cb 0%,#2575fc 100%);color:var(--white);padding:30px;text-align:center;position:relative;overflow:hidden}}.header::before{{content:"""";position:absolute;top:-50%;left:-50%;width:200%;height:200%;background:radial-gradient(circle,rgba(255,255,255,.1) 0%,rgba(255,255,255,0) 70%)}}.logo{{font-size:28px;font-weight:700;display:flex;align-items:center;justify-content:center;margin-bottom:20px}}.logo i{{margin-right:10px;font-size:32px}}.header h1{{font-size:2.5rem;margin-bottom:10px;text-shadow:2px 2px 4px rgba(0,0,0,.3)}}.tagline{{font-size:1.2rem;opacity:.9}}.content{{padding:40px 30px}}.section{{margin-bottom:30px}}h2{{font-size:1.8rem;margin-bottom:20px;color:var(--primary);position:relative;display:inline-block}}h2::after{{content:"""";position:absolute;bottom:-10px;left:0;width:60px;height:4px;background:var(--accent);border-radius:2px}}.section-intro{{font-size:1.1rem;margin-bottom:20px;color:var(--gray)}}.verification-code{{background:var(--light);border-radius:15px;padding:25px;text-align:center;margin:30px 0;border-top:5px solid var(--secondary);box-shadow:var(--shadow)}}.code{{font-size:3rem;font-weight:bold;letter-spacing:10px;color:var(--primary);margin:20px 0;padding:15px;background:rgba(255,255,255,.8);border-radius:10px;display:inline-block}}.instructions{{background:var(--light);border-radius:15px;padding:20px;margin-top:20px}}.instructions ol{{padding-left:20px;margin-top:15px}}.instructions li{{margin-bottom:10px}}.footer{{background:var(--dark);color:#fff;padding:30px;text-align:center;border-radius:0 0 15px 15px}}.footer-links{{display:flex;justify-content:center;gap:20px;margin-bottom:20px}}.footer-links a{{color:rgba(255,255,255,.7);text-decoration:none;transition:all .3s ease}}.footer-links a:hover{{color:#fff}}.copyright{{color:rgba(255,255,255,.6);font-size:.9rem;margin-top:20px}}.note{{font-size:.9rem;color:var(--gray);margin-top:15px;font-style:italic}}.social-icons{{display:flex;justify-content:center;gap:15px;margin:20px 0}}.social-icons a{{display:flex;align-items:center;justify-content:center;width:40px;height:40px;background:rgba(255,255,255,.1);border-radius:50%;color:#fff;font-size:1.2rem;transition:all .3s ease;text-decoration:none}}.social-icons a:hover{{background:var(--primary);transform:translateY(-3px)}}@media (max-width:480px){{.container{{border-radius:10px}}.header{{padding:20px}}.header h1{{font-size:2rem}}.content{{padding:20px 15px}}.code{{font-size:2rem;letter-spacing:5px}}}}</style><link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css""></head><body><div class=""container""><div class=""header""><div class=""logo""><span>天童爱丽丝</span></div><h1>邮箱验证请求</h1></div><div class=""content""><div class=""section""><h2>身份验证程序</h2><p class=""section-intro"">邦邦叭邦~, 爱丽丝检测到用户身份验证请求！</p><p class=""section-intro"">你好, {0}, 欢迎来到千禧年科技学院</p><div class=""verification-code""><p>身份验证密钥：</p><div class=""code"">{1}</div><p>此验证密钥将在 <strong>{2}分钟</strong> 后失效</p></div><div class=""instructions""><h3>验证方法说明：</h3><ol><li>返回登录/注册界面</li><li>在验证码输入区域输入上方6位代码</li><li>点击""登录""或""注册""完成身份认证</li></ol><p class=""note"">如果这不是您发起的验证请求，请忽略此信息。</p></div></div></div><div class=""footer""><div class=""footer-links""><a href=""https://quetzalsidera.me"">开发者博客</a><a href=""https://github.com/QuetzalSidera/QuetzalSidera-Me-FrontBackend"">网站源码</a><a href=""https://open.qurtzalsidera.me/"">开放平台</a></div><div class=""copyright""><p>&copy; 2025 QianShuang | 粤ICP备2025477459号 | 由网易邮箱提供邮件支持</p></div></div></div></body></html>";

    private static MimeMessage NewVerifyMail(string mailTo, string code)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(MailName, MailAccount));
        message.To.Add(new MailboxAddress(string.Empty, mailTo));
        message.Subject = MailSubject;
        // 创建邮件内容
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = string.Format(EmailTemplate, mailTo, code, ExpireMinutes)
        };
        message.Body = bodyBuilder.ToMessageBody();
        return message;
    }

    #endregion
}