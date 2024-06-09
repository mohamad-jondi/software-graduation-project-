// doctor_home_page.dart
import 'package:flutter/material.dart';
import 'package:flutter_app/CalendarPage%20.dart';
import 'package:flutter_app/ContactsPage.dart';
import 'package:flutter_app/NotificationPage.dart';
import 'package:intl/intl.dart';

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
      // New case for ChatPage
      content = Contactspage();
    } else {
      content = HomeScreen(
        upcomingAppointments: upcomingAppointments,
        pendingAppointments: pendingAppointments,
      );
    }

    return Scaffold(
      appBar: AppBar(
        title: Text('Welcome Dr. Ahmad'),
        leading: Builder(
          builder: (context) => IconButton(
            icon: Icon(Icons.person),
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
                  CircleAvatar(
                    radius: 40,
                    backgroundImage: AssetImage('assets/user.jpg'),
                  ),
                  SizedBox(height: 10),
                  Text(
                    'Doctor Name',
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
                // Navigate to edit profile info
              },
            ),
            ListTile(
              leading: Icon(Icons.event),
              title: Text('Manage Appointments'),
              onTap: () {
                // Navigate to manage appointments
              },
            ),
            ListTile(
              leading: Icon(Icons.logout),
              title: Text('Logout'),
              onTap: () {
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

  HomeScreen({
    required this.upcomingAppointments,
    required this.pendingAppointments,
  });

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
    return Padding(
      padding: const EdgeInsets.all(16.0),
      child: ListView(
        children: [
          Text(
            _getNextAppointmentTimeLeft(),
            style: TextStyle(
                fontSize: 18, fontWeight: FontWeight.bold, color: Colors.black),
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
                    'Upcoming Appointments',
                    style: TextStyle(
                        fontSize: 22,
                        fontWeight: FontWeight.bold,
                        color: Color(0xFF199A8E)),
                  ),
                  SizedBox(height: 10),
                  ...upcomingAppointments.map((appointment) {
                    return AppointmentCard(appointment: appointment);
                  }).toList(),
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
                  ...pendingAppointments.map((appointment) {
                    return AppointmentCard(appointment: appointment);
                  }).toList(),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}

class AppointmentCard extends StatelessWidget {
  final Map<String, String> appointment;

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
                    appointment['patient'] ?? 'Unknown',
                    style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 8),
                  Text(
                      'Date & Time: ${appointment['date'] ?? 'N/A'}, ${appointment['time'] ?? 'N/A'}'),
                ],
              ),
            ),
            Container(
              padding: EdgeInsets.symmetric(vertical: 4.0, horizontal: 8.0),
              decoration: BoxDecoration(
                color: (appointment['status'] ?? 'Pending') == 'Confirmed'
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