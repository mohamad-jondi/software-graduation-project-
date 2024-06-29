import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_app/ApiHandler/API.dart';
import 'package:flutter_app/App_Router/App_Router.dart';
import 'package:flutter_app/CheckDoctorCredentialsPage.dart';
import 'package:flutter_app/CheckNurseCredentialsPage.dart';
import 'package:flutter_app/main.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:intl/intl.dart';
import 'package:flutter_app/NotificationPage.dart';
import 'package:flutter_app/ContactsPage.dart';
import 'package:provider/provider.dart';

import 'LoginAndSignup.dart';

class AdminHomePage extends StatefulWidget {
  @override
  _AdminHomePageState createState() => _AdminHomePageState();
}

class _AdminHomePageState extends State<AdminHomePage> {
  int _selectedIndex = 0;
  PageController _pageController = PageController();

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
      _pageController.jumpToPage(index);
    });
  }

  @override
  Widget build(BuildContext context) {
    var user = Provider.of<AppProvider>(context).loggedUser;
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
        backgroundColor: Colors.white,
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
                        'images/admin.png'), // Replace with your image asset
                  ),
                  SizedBox(height: 10),
                  Text(
                    user.name!,
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 24,
                    ),
                  ),
                ],
              ),
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
          ManageDoctorsPage(),
          ManageNursesPage(),
          NotificationPage(),
          Contactspage(),
        ],
      ),
      bottomNavigationBar: BottomNavigationBar(
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: 'Manage Doctors',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person_outline),
            label: 'Manage Nurses',
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

class ManageDoctorsPage extends StatelessWidget {
  final List<Map<String, dynamic>> doctors = [
    {
      "name": "Dr. John Doe",
      "image": 'images/doctor1.png', // Replace with your image asset
      "requestDate": DateTime.now().subtract(Duration(days: 2)),
      "credentials": "PhD in Medicine, Harvard University"
    },
    {
      "name": "Dr. Jane Smith",
      "image": 'images/doctor2.png', // Replace with your image asset
      "requestDate": DateTime.now().subtract(Duration(days: 3)),
      "credentials": "MD, Stanford University"
    },
  ];

  String _formatDate(DateTime date) {
    return DateFormat('yyyy-MM-dd').format(date);
  }

  fetchUnverifiedDoctors() async {
    return await API.apis.getUnverifiedDoctors() as List<dynamic>;
  }

  @override
  Widget build(BuildContext context) {
    return ListView(
      padding: EdgeInsets.all(16.0),
      children: [
        Text(
          'Doctors Pending Approval',
          style: TextStyle(
              fontSize: 22,
              fontWeight: FontWeight.bold,
              color: Color(0xFF199A8E)),
        ),
        SizedBox(height: 10),
        FutureBuilder<dynamic>(
            future: fetchUnverifiedDoctors(),
            builder: (BuildContext context, AsyncSnapshot<dynamic> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return SizedBox(height: 10);
              }
              if (snapshot.hasError) {
                return SizedBox(height: 10);
              }
              if (!snapshot.hasData)
                return SizedBox(
                  height: 10,
                );
              print("data");
              List<dynamic> data = snapshot.data;
              print(data);
              return Column(
                children: data.map((doctor) {
                  return Card(
                    margin: EdgeInsets.symmetric(vertical: 8.0),
                    child: Padding(
                      padding: const EdgeInsets.all(16.0),
                      child: Row(
                        children: [
                          CircleAvatar(
                            radius: 30,
                            backgroundImage: AssetImage(
                              "${API.apis.server}/uploads/DefualtPicture.png}",
                            ),
                          ),
                          SizedBox(width: 16),
                          Expanded(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Text(
                                  doctor['username'],
                                  style: TextStyle(
                                      fontSize: 18,
                                      fontWeight: FontWeight.bold),
                                ),
                                SizedBox(height: 8),
                              ],
                            ),
                          ),
                          ElevatedButton(
                            onPressed: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (context) =>
                                      CheckDoctorCredentialsPage(
                                    doctorName: doctor['username'],
                                    doctorImage:
                                        "${API.apis.server}/uploads/DefualtPicture.png",
                                    doctorCredentials: doctor['credential'],
                                  ),
                                ),
                              );
                            },
                            style: ElevatedButton.styleFrom(
                              backgroundColor: Color(0xFF199A8E),
                              shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(12),
                              ),
                            ),
                            child: Text('Check Credentials',
                                style: TextStyle(color: Colors.white)),
                          ),
                        ],
                      ),
                    ),
                  );
                }).toList(),
              );
            }),
      ],
    );
  }
}

class ManageNursesPage extends StatelessWidget {
  final List<Map<String, dynamic>> nurses = [
    {
      "name": "Nurse Alice Brown",
      "image": 'images/nurse1.png', // Replace with your image asset
      "requestDate": DateTime.now().subtract(Duration(days: 1)),
      "credentials": "BSN, Johns Hopkins University"
    },
    {
      "name": "Nurse Bob Johnson",
      "image": 'images/nurse2.png', // Replace with your image asset
      "requestDate": DateTime.now().subtract(Duration(days: 4)),
      "credentials": "RN, University of Pennsylvania"
    },
  ];

  String _formatDate(DateTime date) {
    return DateFormat('yyyy-MM-dd').format(date);
  }

  @override
  Widget build(BuildContext context) {
    return ListView(
      padding: EdgeInsets.all(16.0),
      children: [
        Text(
          'Nurses Pending Approval',
          style: TextStyle(
              fontSize: 22,
              fontWeight: FontWeight.bold,
              color: Color(0xFF199A8E)),
        ),
        SizedBox(height: 10),
        ...nurses.map((nurse) {
          return Card(
            margin: EdgeInsets.symmetric(vertical: 8.0),
            child: Padding(
              padding: const EdgeInsets.all(16.0),
              child: Row(
                children: [
                  CircleAvatar(
                    radius: 30,
                    backgroundImage: AssetImage(nurse['image']),
                  ),
                  SizedBox(width: 16),
                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          nurse['name'],
                          style: TextStyle(
                              fontSize: 18, fontWeight: FontWeight.bold),
                        ),
                        SizedBox(height: 8),
                        Text(
                          'Request Date: ${_formatDate(nurse['requestDate'])}',
                          style: TextStyle(fontSize: 16),
                        ),
                      ],
                    ),
                  ),
                  ElevatedButton(
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => CheckNurseCredentialsPage(
                            nurseName: nurse['name'],
                            nurseImage: nurse['image'],
                            requestDate: _formatDate(nurse['requestDate']),
                            nurseCredentials: nurse['credentials'],
                          ),
                        ),
                      );
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Color(0xFF199A8E),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),
                    child: Text('Check Credentials',
                        style: TextStyle(color: Colors.white)),
                  ),
                ],
              ),
            ),
          );
        }).toList(),
      ],
    );
  }
}

void main() {
  runApp(MaterialApp(
    home: AdminHomePage(),
  ));
}
