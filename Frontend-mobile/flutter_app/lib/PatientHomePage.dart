import 'package:flutter/material.dart';
import 'package:flutter_app/NotificationPage.dart';
import 'package:flutter_app/ContactsPage.dart';
import 'package:flutter_app/EditProfileDoctor.dart'; // Use the same edit profile page for simplicity
import 'package:flutter_app/PatientAppointmentsPage.dart';
import 'package:flutter_app/PatientHomeScreen.dart';
import 'package:flutter_app/MedicalInformationPage.dart'; // Import the MedicalInformationPage

class PatientHomePage extends StatefulWidget {
  @override
  _PatientHomePageState createState() => _PatientHomePageState();
}

class _PatientHomePageState extends State<PatientHomePage> {
  int _selectedIndex = 0;

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    Widget content;
    if (_selectedIndex == 1) {
      content = PatientAppointmentsPage();
    } else if (_selectedIndex == 2) {
      content = NotificationPage();
    } else if (_selectedIndex == 3) {
      content = Contactspage();
    } else {
      content = PatientHomeScreen();
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
                  CircleAvatar(
                    radius: 40,
                    backgroundImage: AssetImage('images/doctorimage.png'),
                  ),
                  SizedBox(height: 10),
                  Text(
                    'Ahmad Amira',
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
                    builder: (context) =>
                        EditProfileDoctor(), // Reusing the same edit profile page
                  ),
                );
              },
            ),
            ListTile(
              leading: Icon(Icons.event),
              title: Text('View Appointments'),
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => PatientAppointmentsPage(),
                  ),
                );
              },
            ),
            ListTile(
              leading: Icon(Icons.medical_services),
              title: Text('View Medical Information'),
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => MedicalInformationPage(
                      bloodType: 'O+', // Example blood type
                      allergies: ['Pollen', 'Dust', 'Peanuts'], // Example data
                      medications: ['Aspirin', 'Metformin'], // Example data
                      surgeries: ['Appendectomy'], // Example data
                    ),
                  ),
                );
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
            icon: Icon(Icons.event),
            label: 'Appointments',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.notifications),
            label: 'Notifications',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.chat),
            label: 'Chats',
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
