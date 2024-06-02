using System.Text.RegularExpressions;

namespace TestWebApi.Helpers;

public class LoginExtensions
{
    /// <summary>
    ///     비밀번호 체크 정규식
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static bool PasswordCheck(string? password)
    {
        //길이 10자 이상, 숫자1이상, 영문1이상, 특수문자1이상

        //10자리 이상
        if (password != null && password.Length < 10) return false;

        //숫자1이상, 영문1이상, 특수문자1이상
        var regexPass = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,}$",
            RegexOptions.IgnorePatternWhitespace);
        return regexPass.IsMatch(password!);
    }


    public static string CheckEmail(string email)
    {
        if (!string.IsNullOrEmpty(email) && !email.Contains("@"))
            return "이메일 형식이 잘못되었습니다.";

        return string.Empty;
    }

    public static string CheckPassword(string password,
        string passwordAgain
    )
    {
        if (string.IsNullOrEmpty(password))
            return "비밀번호가 입력되지 않았습니다.";
        // if (!PasswordCheck(password))
        //     return "비밀번호는 길이 10자 이상, 숫자1이상, 영문1이상, 특수문자1이상 이어야 합니다.";
        if (password != passwordAgain)
            return "비밀번호와 비밀번호 재입력값이 다릅니다.";
        return string.Empty;
    }

}