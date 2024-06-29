import 'package:flutter/material.dart';
import 'package:flutter_app/ApiHandler/API.dart';
import 'package:flutter_app/AppointmentPage.dart';
import 'package:flutter_app/CalendarPage%20.dart';
import 'package:flutter_app/ContactsPage.dart';
import 'package:flutter_app/EditProfileDoctor.dart';
import 'package:flutter_app/LoginAndSignup.dart';
import 'package:flutter_app/ManageAppointmentsPage.dart';
import 'package:flutter_app/ConfirmedAppointmentsPage.dart';
import 'package:flutter_app/NotificationPage.dart';
import 'package:flutter_app/main.dart';
import 'package:flutter_app/models/AppointmentDTO.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:flutter_app/widgets/ImageNetworkWithFallback.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

import 'App_Router/App_Router.dart';

class DoctorHomePage extends StatefulWidget {
  @override
  _DoctorHomePageState createState() => _DoctorHomePageState();
}

class _DoctorHomePageState extends State<DoctorHomePage> {
  int _selectedIndex = 0;
  final List<Map<String, String>> upcomingAppointments = [
    {
      "patient": "John Doe",
      "time": "10:00 AM",
      "date": "2023-05-01",
      "day": "Monday",
      "status": "Confirmed"
    },
    {
      "patient": "Jane Smith",
      "time": "11:30 AM",
      "date": "2023-05-01",
      "day": "Monday",
      "status": "Confirmed"
    },
  ];

  final List<Map<String, String>> pendingAppointments = [
    {
      "patient": "Alice Brown",
      "time": "Pending",
      "date": "2023-05-02",
      "day": "Tuesday",
      "status": "Pending"
    },
    {
      "patient": "Bob Johnson",
      "time": "Pending",
      "date": "2023-05-02",
      "day": "Tuesday",
      "status": "Pending"
    },
  ];

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  String _getNextAppointmentTimeLeft() {
    if (upcomingAppointments.isEmpty) return "No upcoming appointments.";

    final nextAppointment = upcomingAppointments.first;
    final dateStr = nextAppointment['date']!;
    final timeStr = nextAppointment['time']!;
    final dateTimeStr = '$dateStr $timeStr';
    final dateTime = DateFormat('yyyy-MM-dd hh:mm a').parse(dateTimeStr);

    final now = DateTime.now();
    final difference = dateTime.difference(now);

    if (difference.isNegative) return "No upcoming appointments.";

    final hours = difference.inHours;
    final minutes = difference.inMinutes.remainder(60);

    return 'Your next upcoming appointment is in ${hours}h ${minutes}m.';
  }

  @override
  Widget build(BuildContext context) {
    Widget content;
    if (_selectedIndex == 1) {
      content = NotificationPage();
    } else if (_selectedIndex == 3) {
      content = CalendarPage();
    } else if (_selectedIndex == 2) {
      content = Contactspage();
    } else {
      content = HomeScreen(
        upcomingAppointments: upcomingAppointments,
        pendingAppointments: pendingAppointments,
        onShowAllUpcoming: () {
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => ConfirmedAppointmentsPage(
                confirmedAppointments: upcomingAppointments,
              ),
            ),
          );
        },
        onShowAllPending: () {
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => ManageAppointmentsPage(
                pendingAppointments: pendingAppointments,
              ),
            ),
          );
        },
      );
    }

    return Scaffold(
      appBar: AppBar(
        leading: Builder(
          builder: (context) => IconButton(
            icon: Icon(
              Icons.list,
              size: 50,
              color: Color(0xFF199A8E),
            ),
            onPressed: () {
              Scaffold.of(context).openDrawer();
            },
          ),
        ),
      ),
      drawer: Drawer(
        child: ListView(
          padding: EdgeInsets.zero,
          children: <Widget>[
            DrawerHeader(
              decoration: BoxDecoration(
                color: Color(0xFF199A8E),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  ClipRRect(
                    borderRadius: BorderRadius.circular(80),
                    child: Container(
                        width: 80,
                        height: 80,
                        child: NetworkImageWithFallback(
                          imageUrl:
                              '${API.apis.server}/uploads/${Provider.of<AppProvider>(context).loggedUser.username}.png',
                          fallbackImageUrl:
                              '${API.apis.server}/uploads/DefualtPicture.png',
                        )),
                  ),
                  SizedBox(height: 10),
                  Text(
                    Provider.of<AppProvider>(context).loggedUser.name ?? "",
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 24,
                    ),
                  ),
                ],
              ),
            ),
            ListTile(
              leading: Icon(Icons.edit),
              title: Text('Edit Profile Info'),
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => EditProfileDoctor(),
                  ),
                );
              },
            ),
            ListTile(
              leading: Icon(Icons.event),
              title: Text('Manage Appointments'),
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => ManageAppointmentsPage(
                      pendingAppointments: pendingAppointments,
                    ),
                  ),
                );
              },
            ),
            ListTile(
              leading: Icon(Icons.logout),
              title: Text('Logout'),
              onTap: () {
                AppRouter.router.pushReplace(LoginAndSignup());
                // Handle logout
              },
            ),
          ],
        ),
      ),
      body: content,
      bottomNavigationBar: BottomNavigationBar(
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Home',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.notifications),
            label: 'Notifications',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.chat),
            label: 'Chats',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.calendar_today),
            label: 'Calendar',
          ),
        ],
        currentIndex: _selectedIndex,
        selectedItemColor: Color(0xFF199A8E),
        unselectedItemColor: Colors.grey,
        onTap: _onItemTapped,
      ),
    );
  }
}

class HomeScreen extends StatelessWidget {
  final List<Map<String, String>> upcomingAppointments;
  final List<Map<String, String>> pendingAppointments;
  final VoidCallback onShowAllUpcoming;
  final VoidCallback onShowAllPending;

  HomeScreen({
    required this.upcomingAppointments,
    required this.pendingAppointments,
    required this.onShowAllUpcoming,
    required this.onShowAllPending,
  });

  String _getNextAppointmentTimeLeft(List<Appointment> upcomingAppointments) {
    if (upcomingAppointments.isEmpty) return "No upcoming appointments.";
    upcomingAppointments.sort(
      (a, b) => a.date!.compareTo(b.date!),
    );
    final nextAppointment = upcomingAppointments.first;
    final dateTimeStr = nextAppointment.date!;
    // final dateTime =
    //     DateFormat('yyyy-MM-dd hh:mm a').format(dateTimeStr);

    final now = DateTime.now();
    final difference = dateTimeStr.difference(now);

    if (difference.isNegative) return "No upcoming appointments.";

    final hours = difference.inHours;
    final minutes = difference.inMinutes.remainder(60);

    return 'Your next upcoming appointment is in ${hours}h ${minutes}m.';
  }

  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: [
            ElevatedButton(
                onPressed: () async {
                  await provider.snooze();
                },
                child: Text("Snooze")),
            SizedBox(height: 20),
            Container(
              decoration: BoxDecoration(
                border: Border.all(color: Colors.grey),
                borderRadius: BorderRadius.circular(12),
              ),
              child: Padding(
                padding: const EdgeInsets.all(16.0),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Upcoming Appointments',
                      style: TextStyle(
                          fontSize: 22,
                          fontWeight: FontWeight.bold,
                          color: Color(0xFF199A8E)),
                    ),
                    SizedBox(height: 10),
                    ...provider.aappointments.map((appointment) {
                      return GestureDetector(
                        onTap: () async {
                          provider.selectedAppointment = appointment;
                          await provider.getCase(appointment.caseID ?? 0);
                          await provider
                              .getAllergy(appointment.patientName ?? "");
                          AppRouter.router.push(AppointmentPage(
                              patientName: appointment.patientName!,
                              appointmentDate: appointment.date.toString(),
                              appointmentStatus: appointment.status!,
                              appointment: appointment));
                        },
                        child:
                            AppointmentCard(appointment: appointment.toMap()),
                      );
                    }).toList(),
                    Align(
                      alignment: Alignment.centerRight,
                      child: ElevatedButton(
                        onPressed: onShowAllUpcoming,
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Color(0xFF199A8E),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                        ),
                        child: Text('Show All',
                            style: TextStyle(color: Colors.white)),
                      ),
                    ),
                  ],
                ),
              ),
            ),
            SizedBox(height: 20),
            Container(
              decoration: BoxDecoration(
                border: Border.all(color: Colors.grey),
                borderRadius: BorderRadius.circular(12),
              ),
              child: Padding(
                padding: const EdgeInsets.all(16.0),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Pending Appointments',
                      style: TextStyle(
                          fontSize: 22,
                          fontWeight: FontWeight.bold,
                          color: Color(0xFF199A8E)),
                    ),
                    SizedBox(height: 10),
                    ...provider.pappointments.map((appointment) {
                      return GestureDetector(
                        onTap: () => {
                          AppRouter.router.push(AppointmentPage(
                              patientName: appointment.patientName!,
                              appointmentDate: appointment.date.toString(),
                              appointmentStatus: appointment.status!,
                              appointment: appointment))
                        },
                        child:
                            AppointmentCard(appointment: appointment.toMap()),
                      );
                    }).toList(),
                    Align(
                      alignment: Alignment.centerRight,
                      child: ElevatedButton(
                        onPressed: onShowAllPending,
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Color(0xFF199A8E),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                        ),
                        child: Text('Show All',
                            style: TextStyle(color: Colors.white)),
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      );
    });
  }
}

class AppointmentCard extends StatelessWidget {
  final Map<String, dynamic> appointment;

  AppointmentCard({required this.appointment});

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: EdgeInsets.symmetric(vertical: 8.0),
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    appointment['patientName'] ?? 'Unknown',
                    style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 8),
                  Text(
                    'Date & Status: ${appointment['date'] ?? 'N/A'}, ${appointment['status'] ?? 'N/A'}',
                  ),
                ],
              ),
            ),
            Container(
              padding: EdgeInsets.symmetric(vertical: 4.0, horizontal: 8.0),
              decoration: BoxDecoration(
                color: (appointment['status'] ?? 'Pending') == 'Accepted'
                    ? Colors.green
                    : Colors.orange,
                borderRadius: BorderRadius.circular(12),
              ),
              child: Text(
                appointment['status'] ?? 'Pending',
                style: TextStyle(color: Colors.white),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
