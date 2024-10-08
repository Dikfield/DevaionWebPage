using System;

namespace API.Extensions;

public static class DateTimeExtension
{
  public static int CalculteAge(this DateTime dob)
  {
    var today = DateTime.Now;
    var age = today.Year - dob.Year;

    // Se a data de nascimento ainda não ocorreu neste ano, subtraia um ano da idade
    if (dob.Date > today.AddYears(-age)) age--;

    return age;
  }

  public static DateTime EnsureUtc(this DateTime dateTime)
  {
    if (dateTime.Kind == DateTimeKind.Utc)
      return dateTime;

    if (dateTime.Kind == DateTimeKind.Local)
      return dateTime.ToUniversalTime();

    // Especifica o DateTimeKind como Utc se não estiver definido
    return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
  }

}
