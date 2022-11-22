from datetime import datetime, timezone, timedelta

# CREATING
local_tz = datetime.now(timezone.utc).astimezone().tzinfo
dt = datetime.now(timezone.utc)
dt = datetime.now(local_tz)
dt = datetime(2023, 1, 2, 8, 0, 0, 0, timezone.utc)
dt = datetime(2023, 1, 2, 0, 0, 0, 0, local_tz)
dt = datetime.fromtimestamp(1672646400, timezone.utc)
dt = datetime.fromtimestamp(1672646400, local_tz)
dt = datetime.strptime("2023-01-02T08:00:00.000Z", "%Y-%m-%dT%H:%M:%S.%f%z")
dt = datetime.strptime("2023-01-02T08:00:00.000Z", "%Y-%m-%dT%H:%M:%S.%f%z").astimezone(local_tz)

# TIMEZONE STUFF
print(dt.tzinfo) # 'Pacific Standard Time' or 'UTC'

# EXPORTING
print(dt)                                                            # => 2023-01-02 00:00:00-08:00
print(int(datetime.timestamp(dt)))                                   # => 1672646400
print(dt.astimezone(timezone.utc).strftime("%Y-%m-%dT%H:%M:%S.%fZ")) # => 2023-01-02T08:00:00.000000Z
# Formatting: https://strftime.org/ (note: zero-padding not available on windows)
print(dt.strftime("%d-%b-%Y %I:%M %p"))                              # => 02-Jan-2023 12:00 AM
print(dt.strftime("%A %B %d, %Y %I:%M:%S %p"))                       # => Monday January 02, 2023 12:00:00 AM
print(dt.strftime("%Y-%m-%d_%H:%M:%S"))                              # => 2023-01-02_00:00:00

# COMPONENTS
year = dt.year
month = dt.month
day = dt.day
hour = dt.hour
minute = dt.minute
second = dt.second
day_of_week = dt.weekday() # [0-6], 0 == Monday

# MODIFYING
dt2 = dt # Cloning
dt = dt + timedelta(days=2) # Add 2 days
dt = dt + timedelta(hours=3) # Add 3 hours