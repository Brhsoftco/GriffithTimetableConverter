using GriffithTimetableConverter.Timetable.JsonModel;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System;
using System.IO;
using System.Text;

namespace GriffithTimetableConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //arguments must be supplied
                if (args.Length == 0)

                    //report error
                    Console.WriteLine("Error: No University Timetable JSON Was Provided");
                else
                {
                    //does the target JSON exist before attempting a read?
                    if (File.Exists(args[0]))
                    {
                        //load
                        var json = File.ReadAllText(args[0]);

                        //is it valid on a basic level?
                        if (!string.IsNullOrWhiteSpace(json))
                        {
                            //parse
                            var timetableModel = ScheduleModel.FromJson(json);

                            //validation
                            if (timetableModel != null)
                            {
                                //null validation
                                if (timetableModel.Events?.Length > 0)
                                {
                                    //calender provider
                                    var calendar = new Calendar();

                                    //go through each event
                                    foreach (var e in timetableModel.Events)
                                    {
                                        //print event name
                                        Console.WriteLine("\nFOUND:");
                                        Console.WriteLine($"{e.CourseTitle} ({e.CourseCode})");
                                        Console.WriteLine($"Starts: {e.Start}");
                                        Console.WriteLine($"Ends: {e.End}");
                                        Console.WriteLine($"Location: {e.LocationNumber}");
                                        Console.WriteLine($"Type: {e.TypeName}");

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

                                        //add the final event
                                        calendar.Events.Add(calenderEvent);
                                    }

                                    //serialisation provider
                                    var serializer = new CalendarSerializer();

                                    //serialise to final formatted string
                                    var serializedCalendar = serializer.SerializeToString(calendar);

                                    //calender file
                                    var outputFile =
                                        $@"{Path.GetDirectoryName(args[0])}\{Path.GetFileNameWithoutExtension(args[0])}.ics";

                                    //write to calender file
                                    File.WriteAllText(outputFile, serializedCalendar, new UTF8Encoding(false));

                                    //report success
                                    Console.WriteLine($"\n\nSuccess! Wrote calender file to: {outputFile}");
                                }
                                else
                                {
                                    //report error
                                    Console.WriteLine("Error: Timetable events were null");
                                }
                            }
                            else
                            {
                                //report error
                                Console.WriteLine("Error: Timetable model was null");
                            }
                        }
                        else
                        {
                            //report error
                            Console.WriteLine("Error: Provided JSON file contents were null, empty or whitespace");
                        }
                    }
                    else
                    {
                        //report error
                        Console.WriteLine("Error: Provided timetable JSON file doesn't exist");
                    }
                }

                //pause console
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                //report error to user
                Console.WriteLine(@"Error: Critical execution error");
                Console.WriteLine(ex.ToString());

                //write error to file
                File.WriteAllText(@"lastError.log", ex.ToString());
            }
        }
    }
}