import 'package:flutter/material.dart';
import 'package:flutter_app/AddCredentialsPage.dart';
import 'package:flutter_app/AdminHomePage.dart';
import 'package:flutter_app/DoctorSignup.dart';
import 'package:flutter_app/LoginAndSignup.dart';
import 'package:flutter_app/MotherHomePage.dart';
import 'Wellcome.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: WellcomeScreen(),
    );
  }
}

class WellcomeScreen extends StatefulWidget {
  @override
  _WellcomeScreenState createState() => _WellcomeScreenState();
}

class _WellcomeScreenState extends State<WellcomeScreen> {
  @override
  void initState() {
    super.initState();
    // Navigate to LoginPage after 5 seconds
    Future.delayed(Duration(seconds: 5), () {
      Navigator.of(context).pushReplacement(
        MaterialPageRoute(builder: (context) => MotherHomePage()),
      );
    });
  }

  @override
  Widget build(BuildContext context) {
    return Wellcome(); // This is your welcome page
  }
}
