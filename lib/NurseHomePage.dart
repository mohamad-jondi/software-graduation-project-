import 'package:flutter/material.dart';
import 'package:flutter_app/main.dart';
import 'package:intl/intl.dart';
import 'package:flutter_app/NotificationPage.dart';
import 'package:flutter_app/ContactsPage.dart';

import 'App_Router/App_Router.dart';
import 'LoginAndSignup.dart';

class NurseHomePage extends StatefulWidget {
  @override
  _NurseHomePageState createState() => _NurseHomePageState();
}

class _NurseHomePageState extends State<NurseHomePage> {
  int _selectedIndex = 0;
  PageController _pageController = PageController();

  final List<Map<String, dynamic>> patients = [
    {
      "name": "John Doe",
      "image": 'images/patient1.png', // Replace with your image asset
      "nextCheckIn": DateTime.now().add(Duration(hours: 2, minutes: 30)),
    },
    {
      "name": "Jane Smith",
      "image": 'images/patient2.png', // Replace with your image asset
      "nextCheckIn": DateTime.now().add(Duration(hours: 4, minutes: 45)),
    },
  ];

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
      _pageController.jumpToPage(index);
    });
  }

  void _removePatient(int index) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Remove Patient'),
          content: Text('Are you sure you want to remove this patient?'),
          actions: [
            TextButton(
              child: Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: Text('Remove', style: TextStyle(color: Colors.red)),
              onPressed: () {
                setState(() {
                  patients.removeAt(index);
                });
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  String _timeRemaining(DateTime nextCheckIn) {
    final now = DateTime.now();
    final difference = nextCheckIn.difference(now);
    if (difference.isNegative) return "Check-in overdue";

    final hours = difference.inHours;
    final minutes = difference.inMinutes.remainder(60);
    return '${hours}h ${minutes}m remaining';
  }

  void _showAddPatientDialog() {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Add Patient'),
          content: TextField(
            decoration: InputDecoration(
              hintText: 'Search patient by name',
            ),
            onChanged: (value) {
              // Implement search logic here
            },
          ),
          actions: [
            TextButton(
              child: Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: Text('Add'),
              onPressed: () {
                // Implement add patient logic here
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    Widget homeContent = ListView(
      padding: EdgeInsets.all(16.0),
      children: [
        Text(
          'Your Patients',
          style: TextStyle(
              fontSize: 22,
              fontWeight: FontWeight.bold,
              color: Color(0xFF199A8E)),
        ),
        SizedBox(height: 10),
        ...patients.map((patient) {
          int index = patients.indexOf(patient);
          return GestureDetector(
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => PatientDetailsPage(
                    patientName: patient['name'],
                    nextCheckInDate:
                        DateFormat('yyyy-MM-dd').format(patient['nextCheckIn']),
                    checkInStatus:
                        'Not Completed', // Replace with actual status
                  ),
                ),
              );
            },
            child: Card(
              margin: EdgeInsets.symmetric(vertical: 8.0),
              child: Padding(
                padding: const EdgeInsets.all(16.0),
                child: Row(
                  children: [
                    CircleAvatar(
                      radius: 30,
                      backgroundImage: AssetImage(patient['image']),
                    ),
                    SizedBox(width: 16),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            patient['name'],
                            style: TextStyle(
                                fontSize: 18, fontWeight: FontWeight.bold),
                          ),
                          SizedBox(height: 8),
                          Text(
                            _timeRemaining(patient['nextCheckIn']),
                            style: TextStyle(fontSize: 16),
                          ),
                        ],
                      ),
                    ),
                    IconButton(
                      icon: Icon(Icons.delete, color: Colors.red),
                      onPressed: () => _removePatient(index),
                    ),
                  ],
                ),
              ),
            ),
          );
        }).toList(),
        SizedBox(height: 20),
        ElevatedButton(
          onPressed: _showAddPatientDialog,
          style: ElevatedButton.styleFrom(
            backgroundColor: Color(0xFF199A8E),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12.0),
            ),
            padding: EdgeInsets.symmetric(vertical: 15.0, horizontal: 20.0),
            minimumSize:
                Size(double.infinity, 50), // Make button fill the width
          ),
          child: Text('Add Patient', style: TextStyle(color: Colors.white)),
        ),
      ],
    );

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
                    backgroundImage: AssetImage(
                        'images/nurse.png'), // Replace with your image asset
                  ),
                  SizedBox(height: 10),
                  Text(
                    'Nurse Name',
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
                // Navigate to Edit Profile Page
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
      body: PageView(
        controller: _pageController,
        children: [
          homeContent,
          NotificationPage(),
          Contactspage(),
        ],
      ),
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
        ],
        currentIndex: _selectedIndex,
        selectedItemColor: Color(0xFF199A8E),
        unselectedItemColor: Colors.grey,
        onTap: _onItemTapped,
      ),
    );
  }
}

class PatientDetailsPage extends StatefulWidget {
  final String patientName;
  final String nextCheckInDate;
  String checkInStatus;

  PatientDetailsPage({
    required this.patientName,
    required this.nextCheckInDate,
    required this.checkInStatus,
  });

  @override
  _PatientDetailsPageState createState() => _PatientDetailsPageState();
}

class _PatientDetailsPageState extends State<PatientDetailsPage> {
  void _markAsComplete() {
    setState(() {
      widget.checkInStatus = 'Completed';
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Patient Details', style: TextStyle(color: Colors.white)),
        backgroundColor: Color(0xFF199A8E),
        iconTheme: IconThemeData(color: Colors.white),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            Expanded(
              child: ListView(
                children: [
                  Text(
                    widget.patientName,
                    style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 8),
                  Text(
                    'Next Check-In Date: ${widget.nextCheckInDate}',
                    style: TextStyle(fontSize: 18),
                  ),
                  SizedBox(height: 4),
                  Row(
                    children: [
                      Text(
                        'Status: ',
                        style: TextStyle(fontSize: 18),
                      ),
                      Container(
                        padding: EdgeInsets.symmetric(
                            vertical: 4.0, horizontal: 8.0),
                        decoration: BoxDecoration(
                          color: _getStatusColor(widget.checkInStatus),
                          borderRadius: BorderRadius.circular(12),
                        ),
                        child: Text(
                          widget.checkInStatus,
                          style: TextStyle(color: Colors.white, fontSize: 18),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(height: 16),
                  _buildDescriptionSection(
                    context,
                    description:
                        'This section can contain additional details about the patient or notes from the nurse.',
                  ),
                  SizedBox(height: 16),
                  _buildSection(
                    context,
                    title: 'Allergies',
                    items: ['Pollen', 'Dust', 'Peanuts'], // Dummy data
                  ),
                  SizedBox(height: 16),
                  _buildSection(
                    context,
                    title: 'Medications',
                    items: ['Aspirin', 'Metformin'], // Dummy data
                  ),
                  SizedBox(height: 16),
                  _buildSection(
                    context,
                    title: 'Surgeries',
                    items: ['Appendectomy'], // Dummy data
                  ),
                ],
              ),
            ),
            if (widget.checkInStatus == 'Not Completed')
              ElevatedButton(
                onPressed: _markAsComplete,
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.green,
                  minimumSize:
                      Size(double.infinity, 50), // Make button fill the width
                ),
                child: Text('Mark as Complete',
                    style: TextStyle(color: Colors.white)),
              ),
          ],
        ),
      ),
    );
  }

  Color _getStatusColor(String status) {
    switch (status) {
      case 'Completed':
        return Colors.green;
      case 'Not Completed':
        return Colors.red;
      default:
        return Colors.grey;
    }
  }

  Widget _buildDescriptionSection(BuildContext context,
      {required String description}) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          'Description',
          style: TextStyle(
            fontSize: 20,
            fontWeight: FontWeight.bold,
            color: Color(0xFF199A8E),
          ),
        ),
        SizedBox(height: 8),
        Text(description, style: TextStyle(fontSize: 16)),
      ],
    );
  }

  Widget _buildSection(BuildContext context,
      {required String title, required List<String> items}) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          style: TextStyle(
            fontSize: 20,
            fontWeight: FontWeight.bold,
            color: Color(0xFF199A8E),
          ),
        ),
        ...items.map((item) {
          return Card(
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
            margin: EdgeInsets.symmetric(vertical: 4),
            child: ListTile(
              title: Text(item),
              trailing: IconButton(
                icon: Icon(Icons.delete, color: Colors.red),
                onPressed: () {
                  _showDeleteConfirmationDialog(context, item);
                },
              ),
            ),
          );
        }).toList(),
        Align(
          alignment: Alignment.centerRight,
          child: ElevatedButton(
            style: ElevatedButton.styleFrom(
              backgroundColor: Color(0xFF199A8E), // Button background color
            ),
            onPressed: () {
              // Handle add action
            },
            child: Text('Add', style: TextStyle(color: Colors.white)),
          ),
        ),
      ],
    );
  }

  void _showDeleteConfirmationDialog(BuildContext context, String item) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Delete Confirmation'),
          content: Text('Are you sure you want to delete "$item"?'),
          actions: [
            TextButton(
              child: Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: Text('Delete', style: TextStyle(color: Colors.red)),
              onPressed: () {
                // Handle delete action here
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }
}

void main() {
  runApp(MaterialApp(
    home: NurseHomePage(),
  ));
}
