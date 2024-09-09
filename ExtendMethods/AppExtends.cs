using System.Net;
using Microsoft.AspNetCore.Builder;

namespace AppMvc.Net.ExtendMethods;

public static class AppExtends
{
    public static void AddStatusCodePage (this IApplicationBuilder app)
    {
        app.UseStatusCodePages(appError => {
    appError.Run(async context => {
        var response = context.Response;
        var code = response.StatusCode;
      //var content = "loi";
        var content = @$"<html>
        <head>
            <meta charset='UTF-8'/>
            <title></title>
        </head>
        <body>
            <p style='color:red; font-size: 30px'>
                Co loi xay ra: {code} - {(HttpStatusCode)code}
            </p>
        </body>
        </html";
        await response.WriteAsync(content);
    });
});
    }

}