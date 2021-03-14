using GriffithTimetableConverter.Timetable.JsonModel.Griffith;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;

namespace GriffithTimetableConverter
{
    public static class Extensions
    {
        public static CalendarEvent ToCalendarEvent(this Event e)
        {
            try
            {
                //validation
                if (e != null)
                {
                    //geocoding validation
                    var geocoding = e.LocationGeocode != null
                        ? new GeographicLocation(e.LocationGeocode.Lat, e.LocationGeocode.Lon)
                        : new GeographicLocation(0, 0);

                    //location number validation
                    var locationNumber = !string.IsNullOrWhiteSpace(e.LocationNumber)
                        ? e.LocationNumber
                        : @"TBA";

                    //description
                    var description = $"{e.TypeName} ({e.TypeCode})";

                    //is a URL specified?
                    if (e.Url != null)

                        //append to the description
                        description += $"\n\nURL: {e.Url}";

                    //construct event
                    var calenderEvent = new CalendarEvent
                    {
                        Start = new CalDateTime(e.Start.DateTime),
                        End = new CalDateTime(e.End.DateTime),
                        Location = locationNumber,
                        Transparency = TransparencyType.Opaque,
                        Summary = $"{e.CourseTitle} ({e.CourseCode})",
                        Description = description,
                        GeographicLocation = geocoding
                    };

                    //return fully constructed event
                    return calenderEvent;
                }
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine("Error: Event conversion error");
                Console.WriteLine(ex.ToString());
            }

            //default
            return null;
        }

        public static Calendar ToCalendar(this ScheduleModel s)
        {
            try
            {
                //null validation
                if (s != null)
                {
                    //calendar provider
                    var calendar = new Calendar();

                    //through each event
                    foreach (var e in s.Events)
                    {
                        //construct calender event
                        var calendarEvent = e.ToCalendarEvent();

                        //null validation
                        if (calendarEvent != null)

                            //apply it to the calendar
                            calendar.Events.Add(calendarEvent);
                    }

                    //return fully-constructed calender
                    return calendar;
                }
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine("Error: Schedule conversion error");
                Console.WriteLine(ex.ToString());
            }

            //default
            return null;
        }
    }
}