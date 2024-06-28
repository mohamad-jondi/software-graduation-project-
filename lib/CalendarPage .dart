import 'package:flutter/material.dart';
import 'package:flutter_app/AppointmentPage.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';
import 'package:table_calendar/table_calendar.dart';

class CalendarPage extends StatefulWidget {
  @override
  _CalendarPageState createState() => _CalendarPageState();
}

class _CalendarPageState extends State<CalendarPage> {
  late final ValueNotifier<List<Map<String, String>>> _selectedEvents;
  CalendarFormat _calendarFormat = CalendarFormat.month;
  DateTime _focusedDay = DateTime.now();
  DateTime? _selectedDay;

  Map<DateTime, List<Map<String, String>>> _events = {
    DateTime.utc(2024, 5, 1): [
      {"patient": "John Doe", "time": "10:00 AM", "status": "Confirmed"},
      {"patient": "Jane Smith", "time": "11:30 AM", "status": "Confirmed"},
    ],
    DateTime.utc(2024, 5, 2): [
      {"patient": "Alice Brown", "time": "Pending", "status": "Pending"},
      {"patient": "Bob Johnson", "time": "Pending", "status": "Pending"},
    ],
    DateTime.utc(2024, 5, 1): [
      {"patient": "Test Patient", "time": "9:00 AM", "status": "Confirmed"},
    ],
  };

  @override
  void initState() {
    super.initState();
    _selectedDay = _focusedDay;
    _selectedEvents = ValueNotifier(_getEventsForDay(_selectedDay!));
  }

  @override
  void dispose() {
    _selectedEvents.dispose();
    super.dispose();
  }

  List<Map<String, String>> _getEventsForDay(DateTime day) {
    final normalizedDay = DateTime.utc(day.year, day.month, day.day);
    return _events[normalizedDay] ?? [];
  }

  void _onDaySelected(DateTime selectedDay, DateTime focusedDay) {
    if (!isSameDay(_selectedDay, selectedDay)) {
      setState(() {
        _selectedDay = selectedDay;
        _focusedDay = focusedDay;
      });

      _selectedEvents.value = _getEventsForDay(selectedDay);
    }
  }

  Color _getStatusColor(String status) {
    switch (status) {
      case "Confirmed":
        return Colors.green;
      case "Pending":
        return Colors.orange;
      default:
        return Colors.grey;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      _events = {};
      for (int i = 0; i < provider.aappointments.length; i++) {
        {
          if (_events[DateTime.utc(
                  provider.aappointments[i].date!.year,
                  provider.aappointments[i].date!.month,
                  provider.aappointments[i].date!.day)] ==
              null) {
            _events[DateTime.utc(
                provider.aappointments[i].date!.year,
                provider.aappointments[i].date!.month,
                provider.aappointments[i].date!.day)] = [
              provider.aappointments[i].toAMap()
            ];
          } else {
            var x = _events[DateTime.utc(
                provider.aappointments[i].date!.year,
                provider.aappointments[i].date!.month,
                provider.aappointments[i].date!.day)];
            x!.add(provider.aappointments[i].toAMap());
            _events[DateTime.utc(
                provider.aappointments[i].date!.year,
                provider.aappointments[i].date!.month,
                provider.aappointments[i].date!.day)] = x;
          }
        }
      }
      for (int i = 0; i < provider.pappointments.length; i++) {
        {
          if (_events[DateTime.utc(
                  provider.pappointments[i].date!.year,
                  provider.pappointments[i].date!.month,
                  provider.pappointments[i].date!.day)] ==
              null) {
            _events[DateTime.utc(
                provider.pappointments[i].date!.year,
                provider.pappointments[i].date!.month,
                provider.pappointments[i].date!.day)] = [
              provider.pappointments[i].toAMap()
            ];
          } else {
            var x = _events[DateTime.utc(
                provider.pappointments[i].date!.year,
                provider.pappointments[i].date!.month,
                provider.pappointments[i].date!.day)];
            x!.add(provider.pappointments[i].toAMap());
            _events[DateTime.utc(
                provider.pappointments[i].date!.year,
                provider.pappointments[i].date!.month,
                provider.pappointments[i].date!.day)] = x;
          }
        }
      }
      return Scaffold(
        appBar: AppBar(
          title: Text(
            'Your Calendar',
            style: TextStyle(color: Color(0xFF199A8E)),
          ),
          iconTheme: IconThemeData(color: Color(0xFF199A8E)),
        ),
        body: Column(
          children: [
            TableCalendar<Map<String, String>>(
              firstDay: DateTime.utc(2020, 1, 1),
              lastDay: DateTime.utc(2030, 1, 1),
              focusedDay: _focusedDay,
              selectedDayPredicate: (day) => isSameDay(_selectedDay, day),
              calendarFormat: _calendarFormat,
              eventLoader: _getEventsForDay,
              startingDayOfWeek: StartingDayOfWeek.monday,
              calendarStyle: CalendarStyle(
                outsideDaysVisible: false,
              ),
              onDaySelected: (selectedDay, focusedDay) {
                if (!isSameDay(_selectedDay, selectedDay)) {
                  setState(() {
                    _selectedDay = selectedDay;
                    _focusedDay = focusedDay;
                  });

                  _selectedEvents.value = _getEventsForDay(selectedDay);
                }
              },
              onFormatChanged: (format) {
                if (_calendarFormat != format) {
                  setState(() {
                    _calendarFormat = format;
                  });
                }
              },
              onPageChanged: (focusedDay) {
                _focusedDay = focusedDay;
              },
              calendarBuilders: CalendarBuilders(
                markerBuilder: (context, date, events) {
                  if (events.isNotEmpty) {
                    return Positioned(
                      right: 1,
                      bottom: 1,
                      child: Container(
                        width: 16,
                        height: 16,
                        decoration: BoxDecoration(
                          color: Color(0xFF199A8E),
                          shape: BoxShape.circle,
                        ),
                        child: Center(
                          child: Text(
                            '${events.length}',
                            style: TextStyle().copyWith(
                              color: Colors.white,
                              fontSize: 12,
                            ),
                          ),
                        ),
                      ),
                    );
                  }
                  return null;
                },
              ),
            ),
            const SizedBox(height: 8.0),
            Expanded(
              child: ValueListenableBuilder<List<Map<String, String>>>(
                valueListenable: _selectedEvents,
                builder: (context, value, _) {
                  return ListView.builder(
                    itemCount: value.length,
                    itemBuilder: (context, index) {
                      final event = value[index];
                      return Card(
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(12),
                          side: BorderSide(color: Colors.grey),
                        ),
                        margin:
                            EdgeInsets.symmetric(vertical: 4, horizontal: 16),
                        child: ListTile(
                          contentPadding: EdgeInsets.all(8),
                          title: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    event['patient']!,
                                    style:
                                        TextStyle(fontWeight: FontWeight.bold),
                                  ),
                                  Text(
                                    _selectedDay.toString(),
                                  ),
                                ],
                              ),
                              Container(
                                padding: EdgeInsets.symmetric(
                                    vertical: 4.0, horizontal: 8.0),
                                decoration: BoxDecoration(
                                  color: _getStatusColor(event['status']!),
                                  borderRadius: BorderRadius.circular(12),
                                ),
                                child: Text(
                                  event['status']!,
                                  style: TextStyle(color: Colors.white),
                                ),
                              ),
                            ],
                          ),
                          onTap: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => AppointmentPage(
                                  patientName: event['patient']!,
                                  appointmentDate: _selectedDay.toString(),
                                  appointmentStatus: event['status']!,
                                  appointment: provider.aappointments.where((e)=>e.appointmentId.toString()==event['appointmentId']).toList().length==0?provider.pappointments.firstWhere((e)=>e.appointmentId.toString()==event['appointmentId']):provider.aappointments.firstWhere((e)=>e.appointmentId.toString()==event['appointmentId']),

                                ),
                              ),
                            );
                          },
                        ),
                      );
                    },
                  );
                },
              ),
            ),
          ],
        ),
      );
    });
  }
}
