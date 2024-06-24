import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:flutter_app/NotificationPage.dart';
import 'package:flutter_app/ContactsPage.dart';

class MotherHomePage extends StatefulWidget {
  @override
  _MotherHomePageState createState() => _MotherHomePageState();
}

class _MotherHomePageState extends State<MotherHomePage> {
  int _selectedIndex = 0;
  PageController _pageController = PageController();

  final List<Map<String, dynamic>> children = [
    {
      "name": "Tom",
      "image": 'images/child1.png', // Replace with your image asset
      "vaccinations": ['Polio', 'MMR', 'Hepatitis B'], // Dummy data
      "nextVaccine": DateTime.now().add(Duration(days: 30)), // Example date
    },
    {
      "name": "Jerry",
      "image": 'images/child2.png', // Replace with your image asset
      "vaccinations": ['Polio', 'MMR'], // Dummy data
      "nextVaccine": DateTime.now().add(Duration(days: 60)), // Example date
    },
  ];

  void _onItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
      _pageController.jumpToPage(index);
    });
  }

  void _removeChild(int index) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Remove Child'),
          content: Text('Are you sure you want to remove this child?'),
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
                  children.removeAt(index);
                });
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  String _timeRemaining(DateTime nextVaccine) {
    final now = DateTime.now();
    final difference = nextVaccine.difference(now);
    if (difference.isNegative) return "Vaccine overdue";

    final days = difference.inDays;
    return '$days days remaining';
  }

  void _showAddChildDialog() {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Add Child'),
          content: TextField(
            decoration: InputDecoration(
              hintText: 'Enter child name',
            ),
            onChanged: (value) {
              // Implement logic to handle the new child's name
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
                // Implement add child logic here
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
          'Your Children',
          style: TextStyle(
              fontSize: 22,
              fontWeight: FontWeight.bold,
              color: Color(0xFF199A8E)),
        ),
        SizedBox(height: 10),
        ...children.map((child) {
          int index = children.indexOf(child);
          return GestureDetector(
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => ChildDetailsPage(
                    childName: child['name'],
                    nextVaccineDate:
                        DateFormat('yyyy-MM-dd').format(child['nextVaccine']),
                    vaccinations:
                        child['vaccinations'], // Replace with actual data
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
                      backgroundImage: AssetImage(child['image']),
                    ),
                    SizedBox(width: 16),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            child['name'],
                            style: TextStyle(
                                fontSize: 18, fontWeight: FontWeight.bold),
                          ),
                          SizedBox(height: 8),
                          Text(
                            _timeRemaining(child['nextVaccine']),
                            style: TextStyle(fontSize: 16),
                          ),
                        ],
                      ),
                    ),
                    IconButton(
                      icon: Icon(Icons.delete, color: Colors.red),
                      onPressed: () => _removeChild(index),
                    ),
                  ],
                ),
              ),
            ),
          );
        }).toList(),
        SizedBox(height: 20),
        ElevatedButton(
          onPressed: _showAddChildDialog,
          style: ElevatedButton.styleFrom(
            backgroundColor: Color(0xFF199A8E),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12.0),
            ),
            padding: EdgeInsets.symmetric(vertical: 15.0, horizontal: 20.0),
            minimumSize:
                Size(double.infinity, 50), // Make button fill the width
          ),
          child: Text('Add Child', style: TextStyle(color: Colors.white)),
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
                        'images/mother.png'), // Replace with your image asset
                  ),
                  SizedBox(height: 10),
                  Text(
                    'Mother Name',
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

class ChildDetailsPage extends StatelessWidget {
  final String childName;
  final String nextVaccineDate;
  final List<String> vaccinations;

  const ChildDetailsPage({
    required this.childName,
    required this.nextVaccineDate,
    required this.vaccinations,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Child Details', style: TextStyle(color: Colors.white)),
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
                    childName,
                    style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 8),
                  Text(
                    'Next Vaccine Date: $nextVaccineDate',
                    style: TextStyle(fontSize: 18),
                  ),
                  SizedBox(height: 16),
                  _buildSection(
                    context,
                    title: 'Vaccinations',
                    items: vaccinations,
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
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
    home: MotherHomePage(),
  ));
}
