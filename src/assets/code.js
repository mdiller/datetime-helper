// CREATING
var dt;
dt = new Date();
dt = new Date(2023, 0, 2, 0, 0, 0, 0); // note month is zero-indexed
dt = new Date(1672646400 * 1000);
dt = new Date("2023-01-02T08:00:00.000Z");

// TIMEZONE
// JavaScript doesn't store timezone information on the Date object.
// - all dates are stored internally as UTC
// - input is assumed to be local time if not clear when parsing

// EXPORTING
console.log(dt);                            // => Mon Jan 02 2023 00:00:00 GMT-0800 (Pacific Standard Time)
console.log(parseInt(dt.getTime() / 1000)); // => 1672646400
console.log(dt.toISOString());              // => 2023-01-02T08:00:00.000Z
// JavaScript doesn't have a native full-fledged date formatter
// - you can use the limited .toLocaleDateString()
// - you can write your own custom formatter
// - you can use an external library like moment.js

// COMPONENTS
var year = dt.getFullYear();
var month = dt.getMonth(); // note month is zero-indexed
var day = dt.getDate();
var hours = dt.getHours();
var minutes = dt.getMinutes();
var seconds = dt.getSeconds();
var day_of_week = dt.getDay(); // [0-6], 0 == Sunday
// Note this will get the local components 
// - use getUTCMonth() etc to get UTC values

// MODIFYING
var dt2 = new Date(dt.getTime()); // Cloning
dt = new Date(dt.getTime() + (1000 * 60 * 60 * 24 * 2)); // Add 2 days
dt = new Date(dt.getTime() + (1000 * 60 * 60 * 3)); // Add 3 hours