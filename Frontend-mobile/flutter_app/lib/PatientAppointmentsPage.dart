import 'package:flutter/material.dart';

class PatientAppointmentsPage extends StatefulWidget {
  @override
  _PatientAppointmentsPageState createState() =>
      _PatientAppointmentsPageState();
}

class _PatientAppointmentsPageState extends State<PatientAppointmentsPage> {
  String _selectedTab = 'Upcoming';

  final List<Map<String, String>> upcomingAppointments = [
    {
      "doctor": "Dr. Marcus Horizon",
      "specialty": "Cardiologist",
      "date": "26/06/2022",
      "time": "10:30 AM",
      "status": "Confirmed",
      "image": "assets/doctorimage.png"
    },
    {
      "doctor": "Dr. Alysa Hana",
      "specialty": "Psychiatrist",
      "date": "28/06/2022",
      "time": "2:00 PM",
      "status": "Confirmed",
      "image": "assets/doctorimage.png"
    },
  ];

  final List<Map<String, String>> completedAppointments = [
    {
      "doctor": "Dr. Marcus Horizon",
      "specialty": "Cardiologist",
      "date": "20/06/2022",
      "time": "10:30 AM",
      "status": "Completed",
      "image": "assets/doctorimage.png"
    },
    {
      "doctor": "Dr. Alysa Hana",
      "specialty": "Psychiatrist",
      "date": "22/06/2022",
      "time": "2:00 PM",
      "status": "Completed",
      "image": "assets/doctorimage.png"
    },
  ];

  final List<Map<String, String>> canceledAppointments = [
    {
      "doctor": "Dr. Marcus Horizon",
      "specialty": "Cardiologist",
      "date": "18/06/2022",
      "time": "10:30 AM",
      "status": "Canceled",
      "image": "assets/doctorimage.png"
    },
    {
      "doctor": "Dr. Alysa Hana",
      "specialty": "Psychiatrist",
      "date": "19/06/2022",
      "time": "2:00 PM",
      "status": "Canceled",
      "image": "assets/doctorimage.png"
    },
  ];

  List<Map<String, String>> get _appointments {
    switch (_selectedTab) {
      case 'Completed':
        return completedAppointments;
      case 'Canceled':
        return canceledAppointments;
      default:
        return upcomingAppointments;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Appointments'),
      ),
      body: Column(
        children: [
          SizedBox(height: 10),
          ToggleButtons(
            isSelected: [
              _selectedTab == 'Upcoming',
              _selectedTab == 'Completed',
              _selectedTab == 'Canceled'
            ],
            onPressed: (int index) {
              setState(() {
                if (index == 0) {
                  _selectedTab = 'Upcoming';
                } else if (index == 1) {
                  _selectedTab = 'Completed';
                } else if (index == 2) {
                  _selectedTab = 'Canceled';
                }
              });
            },
            children: [
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Text('Upcoming'),
              ),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Text('Completed'),
              ),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 16.0),
                child: Text('Canceled'),
              ),
            ],
            color: Colors.black,
            selectedColor: Colors.white,
            fillColor: Color(0xFF199A8E),
            borderRadius: BorderRadius.circular(8.0),
          ),
          Expanded(
            child: ListView.builder(
              padding: EdgeInsets.all(16.0),
              itemCount: _appointments.length,
              itemBuilder: (context, index) {
                final appointment = _appointments[index];
                return Card(
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                  margin: EdgeInsets.symmetric(vertical: 8.0),
                  child: Padding(
                    padding: const EdgeInsets.all(16.0),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Row(
                          children: [
                            CircleAvatar(
                              backgroundImage:
                                  AssetImage(appointment['image']!),
                              radius: 30,
                            ),
                            SizedBox(width: 16),
                            Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Text(
                                  appointment['doctor']!,
                                  style: TextStyle(
                                      fontSize: 18,
                                      fontWeight: FontWeight.bold),
                                ),
                                Text(
                                  appointment['specialty']!,
                                  style: TextStyle(color: Colors.grey),
                                ),
                              ],
                            ),
                          ],
                        ),
                        SizedBox(height: 10),
                        Row(
                          children: [
                            Icon(Icons.calendar_today,
                                size: 16, color: Colors.grey),
                            SizedBox(width: 8),
                            Text(
                              appointment['date']!,
                              style: TextStyle(color: Colors.grey),
                            ),
                            SizedBox(width: 16),
                            Icon(Icons.access_time,
                                size: 16, color: Colors.grey),
                            SizedBox(width: 8),
                            Text(
                              appointment['time']!,
                              style: TextStyle(color: Colors.grey),
                            ),
                            SizedBox(width: 16),
                            Icon(
                              appointment['status'] == 'Completed'
                                  ? Icons.check_circle
                                  : appointment['status'] == 'Canceled'
                                      ? Icons.cancel
                                      : Icons.check_circle,
                              size: 16,
                              color: appointment['status'] == 'Completed'
                                  ? Colors.green
                                  : appointment['status'] == 'Canceled'
                                      ? Colors.red
                                      : Colors.green,
                            ),
                            SizedBox(width: 8),
                            Text(
                              appointment['status']!,
                              style: TextStyle(
                                  color: appointment['status'] == 'Completed'
                                      ? Colors.green
                                      : appointment['status'] == 'Canceled'
                                          ? Colors.red
                                          : Colors.green),
                            ),
                          ],
                        ),
                        SizedBox(height: 10),
                        if (_selectedTab == 'Upcoming')
                          Row(
                            mainAxisAlignment: MainAxisAlignment.end,
                            children: [
                              ElevatedButton(
                                onPressed: () {
                                  // Handle cancel action
                                },
                                style: ElevatedButton.styleFrom(
                                  backgroundColor: Colors.grey[200],
                                  shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(8),
                                  ),
                                ),
                                child: Text('Cancel'),
                              ),
                              SizedBox(width: 16),
                              ElevatedButton(
                                onPressed: () {
                                  // Handle reschedule action
                                },
                                style: ElevatedButton.styleFrom(
                                  backgroundColor: Color(0xFF199A8E),
                                  shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(8),
                                  ),
                                ),
                                child: Text('Reschedule',
                                    style: TextStyle(color: Colors.white)),
                              ),
                            ],
                          ),
                        if (_selectedTab == 'Completed')
                          Row(
                            mainAxisAlignment: MainAxisAlignment.end,
                            children: [
                              ElevatedButton(
                                onPressed: () {
                                  // Handle get second opinion action
                                },
                                style: ElevatedButton.styleFrom(
                                  backgroundColor: Color(0xFF199A8E),
                                  shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(8),
                                  ),
                                ),
                                child: Text('Get Second Opinion',
                                    style: TextStyle(color: Colors.white)),
                              ),
                            ],
                          ),
                      ],
                    ),
                  ),
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}
