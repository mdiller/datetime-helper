// CREATING
DateTime dt;
dt = DateTime.UtcNow;
dt = DateTime.Now;
dt = new DateTime(2023, 1, 2, 8, 0, 0, DateTimeKind.Utc);
dt = new DateTime(2023, 1, 2, 0, 0, 0, DateTimeKind.Local);
dt = DateTimeOffset.FromUnixTimeSeconds(1672646400).DateTime.ToUniversalTime();
dt = DateTimeOffset.FromUnixTimeSeconds(1672646400).DateTime.ToLocalTime();
dt = DateTime.Parse("2023-01-02T08:00:00.000Z").ToUniversalTime();
dt = DateTime.Parse("2023-01-02T08:00:00.000Z");

// TIMEZONE
Console.WriteLine(dt.Kind); // 'Local' or 'Utc'

// EXPORTING
Console.WriteLine(dt);                                          // =>  1/2/2023 12:00:00 AM
Console.WriteLine(new DateTimeOffset(dt).ToUnixTimeSeconds());  // =>  1672646400
Console.WriteLine(dt.ToUniversalTime().ToString("o"));          // =>  2023-01-02T08:00:00.0000000Z
// Formatting: https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
Console.WriteLine(dt.ToString("d-MMM-yyyy h:mm tt"));           // =>  2-Jan-2023 12:00 AM
Console.WriteLine(dt.ToString("dddd MMMM d, yyyy h:mm:ss tt")); // =>  Sunday January 2, 2023 12:00:00 AM
Console.WriteLine(dt.ToString("yyyy-MM-dd_HH:mm:ss"));          // =>  2023-01-02_00:00:00

// COMPONENTS
int year = dt.Year;
int month = dt.Month;
int day = dt.Day;
int hour = dt.Hour;
int minute = dt.Minute;
int second = dt.Second;
DayOfWeek day_of_week = dt.DayOfWeek; // Enum

// MODIFYING
DateTime dt2 = dt; // Cloning
dt = dt.AddDays(2); // Add 2 days
dt = dt.AddHours(3); // Add 3 hours