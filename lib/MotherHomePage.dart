import 'dart:developer';

import 'package:flutter/material.dart';
import 'package:flutter_app/ApiHandler/API.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

import 'App_Router/App_Router.dart';
import 'LoginAndSignup.dart';

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
      "image": 'images/child1.png',
      "vaccinations": ['Polio', 'MMR', 'Hepatitis B'],
      "nextVaccine": DateTime.now().add(Duration(days: 30)),
    },
    {
      "name": "Jerry",
      "image": 'images/child2.png',
      "vaccinations": ['Polio', 'MMR'],
      "nextVaccine": DateTime.now().add(Duration(days: 60)),
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
    final _formKey = GlobalKey<FormState>();
    String name = '';
    String gender = '';
    double height = 0;
    double weight = 0;
    DateTime dob = DateTime.now();
    TextEditingController dobController = TextEditingController();

    Future<void> _selectDate(BuildContext context) async {
      final DateTime? picked = await showDatePicker(
        context: context,
        initialDate: dob,
        firstDate: DateTime(2000),
        lastDate: DateTime.now(),
      );
      if (picked != null && picked != dob) {
        setState(() {
          dob = picked;
          dobController.text = DateFormat('yyyy-MM-dd').format(dob);
        });
      }
    }

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Add Child'),
          content: SingleChildScrollView(
            child: Form(
              key: _formKey,
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextFormField(
                    decoration: InputDecoration(
                      labelText: 'Name',
                    ),
                    onChanged: (value) {
                      name = value;
                    },
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Please enter a name';
                      }
                      return null;
                    },
                  ),
                  TextFormField(
                    decoration: InputDecoration(
                      labelText: 'Gender',
                    ),
                    onChanged: (value) {
                      gender = value;
                    },
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Please enter a gender';
                      }
                      return null;
                    },
                  ),
                  TextFormField(
                    decoration: InputDecoration(
                      labelText: 'Height (cm)',
                    ),
                    keyboardType: TextInputType.number,
                    onChanged: (value) {
                      height = double.tryParse(value) ?? 0;
                    },
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Please enter height';
                      }
                      return null;
                    },
                  ),
                  TextFormField(
                    decoration: InputDecoration(
                      labelText: 'Weight (kg)',
                    ),
                    keyboardType: TextInputType.number,
                    onChanged: (value) {
                      weight = double.tryParse(value) ?? 0;
                    },
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Please enter weight';
                      }
                      return null;
                    },
                  ),
                  TextFormField(
                    controller: dobController,
                    decoration: InputDecoration(
                      labelText: 'Date of Birth',
                    ),
                    readOnly: true,
                    onTap: () => _selectDate(context),
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Please enter date of birth';
                      }
                      return null;
                    },
                  ),
                ],
              ),
            ),
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
              onPressed: () async {
                if (_formKey.currentState!.validate()) {
                  setState(() {
                    children.add({
                      "name": name,
                      "image": 'images/child_default.png',
                      "vaccinations": [],
                      "nextVaccine": dob,
                    });
                  });
                  final res = await API.apis.addChild(
                      Provider.of<AppProvider>(context, listen: false)
                          .loggedUser
                          .username!,
                      <String, dynamic>{
                        "name": name,
                        "gender": gender == 'male' ? 0 : 1,
                        'dateOfBirth':
                            DateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'")
                                .format(dob.toUtc()),
                        "latestRecordedHeight": height,
                        "latestRecordedWeight": weight
                      });
                  log(res.body);
                  Navigator.of(context).pop();
                }
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
        ...Provider.of<AppProvider>(context).children.map((child) {
          int index = Provider.of<AppProvider>(context).children.indexOf(child);
          return GestureDetector(
            onTap: () {
              Provider.of<AppProvider>(context, listen: false).selectedChild =
                  child;
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => ChildDetailsPage(
                    childName: child.name ?? "",
                    nextVaccineDate: DateFormat('yyyy-MM-dd')
                        .format(child.dateOfBirth ?? DateTime.now()),
                    vaccinations: [""],
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
                      backgroundImage: NetworkImage(
                          API.apis.server + "/uploads/DefualtPicture.png"),
                    ),
                    SizedBox(width: 16),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            child.name ?? "",
                            style: TextStyle(
                                fontSize: 18, fontWeight: FontWeight.bold),
                          ),
                          SizedBox(height: 8),
                          Text(
                            _timeRemaining(
                                DateTime.now().subtract(Duration(days: 9))),
                            style: TextStyle(fontSize: 16),
                          ),
                        ],
                      ),
                    ),
                    // IconButton(
                    //   icon: Icon(Icons.delete, color: Colors.red),
                    //   onPressed: () => _removeChild(index),
                    // ),
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
            minimumSize: Size(double.infinity, 50),
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
                    backgroundImage: AssetImage('images/mother.png'),
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
          // NotificationPage(),
          // ContactsPage(),
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
    String n = "No Vaccinations left";
    Provider.of<AppProvider>(context, listen: false)
        .selectedChild!
        .vaccination!
        .sort((a, b) => a.administeredDate!.compareTo(b.administeredDate!));
    if (Provider.of<AppProvider>(context, listen: false)
            .selectedChild!
            .vaccination!
            .length !=
        0) {
      for (int i = 0;
          i <
              Provider.of<AppProvider>(context, listen: false)
                  .selectedChild!
                  .vaccination!
                  .length;
          i++) {
        if (Provider.of<AppProvider>(context, listen: false)
                .selectedChild!
                .vaccination![i]
                .administeredDate!
                .compareTo(DateTime.now()) >
            0) {
          n = "Next Vaccine Date: " +
              Provider.of<AppProvider>(context, listen: false)
                  .selectedChild!
                  .vaccination![i]
                  .administeredDate!
                  .difference(DateTime.now())
                  .inDays
                  .toString() +
              " Days";
        }
      }
    }
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
                    '$n',
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
        ...Provider.of<AppProvider>(context)
            .selectedChild!
            .vaccination!
            .map((item) {
          return Card(
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
            margin: EdgeInsets.symmetric(vertical: 4),
            child: ListTile(
              title: Text(item.name ?? ""),
              trailing: IconButton(
                icon: Icon(Icons.delete, color: Colors.red),
                onPressed: () async {
                  final res =
                      await Provider.of<AppProvider>(context, listen: false)
                          .deleteVac(item.vaccinationID ?? 0);
                },
              ),
            ),
          );
        }).toList(),
        Align(
          alignment: Alignment.centerRight,
          child: ElevatedButton(
            style: ElevatedButton.styleFrom(
              backgroundColor: Color(0xFF199A8E),
            ),
            onPressed: () {
              String name = "";
              String description = "";
              String shotsLeft = "";
              TextEditingController dobController = TextEditingController();
              DateTime administeredDate = DateTime.now();
              Future<void> _selectDate(BuildContext context) async {
                final DateTime? picked = await showDatePicker(
                  context: context,
                  initialDate: administeredDate,
                  firstDate: DateTime(2000),
                  lastDate: DateTime.now().add(Duration(days: 365)),
                );
                if (picked != null && picked != administeredDate) {
                  administeredDate = picked;
                  Provider.of<AppProvider>(context, listen: false)
                      .administeredDateController
                      .text = DateFormat('yyyy-MM-dd').format(administeredDate);
                  dobController.text =
                      DateFormat('yyyy-MM-dd').format(administeredDate);
                }
              }

              showDialog(
                  context: context,
                  builder: (context) => AlertDialog(
                        title: Text("Add new Vaccination"),
                        content: SingleChildScrollView(
                          child: Column(
                            children: [
                              TextFormField(
                                decoration: InputDecoration(
                                  labelText: 'Name',
                                ),
                                onChanged: (value) {
                                  name = value;
                                },
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    return 'Please enter a name';
                                  }
                                  return null;
                                },
                              ),
                              TextFormField(
                                decoration: InputDecoration(
                                  labelText: 'Description',
                                ),
                                onChanged: (value) {
                                  description = value;
                                },
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    return 'Please enter a description';
                                  }
                                  return null;
                                },
                              ),
                              TextFormField(
                                keyboardType: TextInputType.number,
                                decoration: InputDecoration(
                                  labelText: 'Shots Left',
                                ),
                                onChanged: (value) {
                                  shotsLeft = value;
                                },
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    return 'Please enter a number';
                                  }
                                  return null;
                                },
                              ),
                              TextFormField(
                                controller: Provider.of<AppProvider>(context)
                                    .administeredDateController,
                                decoration: InputDecoration(
                                  labelText: 'Date of Birth',
                                ),
                                readOnly: true,
                                onTap: () => _selectDate(context),
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    return 'Please enter date ';
                                  }
                                  return null;
                                },
                              ),
                            ],
                          ),
                        ),
                        actions: [
                          ElevatedButton(
                              onPressed: () async {
                                final res = await Provider.of<AppProvider>(
                                        context,
                                        listen: false)
                                    .addVacc(<String, dynamic>{
                                  "name": name,
                                  "administeredDate":
                                      DateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'")
                                          .format(administeredDate),
                                  "description": description,
                                  "vaccineStatus": administeredDate
                                              .compareTo(DateTime.now()) >
                                          0
                                      ? 0
                                      : 1,
                                  "shotsLeft": int.parse(shotsLeft)
                                });
                                log(res.body);
                              },
                              child: Text("add"))
                        ],
                      ));
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
