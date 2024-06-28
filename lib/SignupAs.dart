import 'package:flutter/material.dart';
import 'package:flutter_app/App_Router/App_Router.dart';
import 'package:flutter_app/DoctorHomePage.dart';
import 'package:flutter_app/DoctorSignup.dart';
import 'package:flutter_app/MotherHomePage.dart';
import 'package:flutter_app/NurseHomePage.dart';
import 'package:flutter_app/PatientHomePage.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';

class SignupAs extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        backgroundColor: Color(0xFF199A8E),
        body: Column(
          children: [
            Expanded(
              flex: 2, // Lesser flex value to make the image smaller
              child: Image.asset(
                'images/SignupAs.png', // Replace with your image asset
                fit: BoxFit.fill,
                width: 400,
              ),
            ),
            Expanded(
              flex: 3, // Greater flex value to make the container larger
              child: Container(
                width: double.infinity,
                padding: EdgeInsets.all(20.0),
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.vertical(
                    top: Radius.circular(20.0),
                  ),
                ),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Image.asset(
                      'images/Vector.png', // Replace with your logo asset
                      height: 80,
                    ),
                    SizedBox(height: 30),
                    Text(
                      'Sign up as',
                      style: TextStyle(
                        color: Color(0xFF199A8E),
                        fontSize: 24,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    SizedBox(height: 20),
                    _buildSignUpButton(
                      icon: Icons.medical_services,
                      text: 'Sign up as a Doctor',
                      onPressed: () {
                        provider.userType = 0;
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => DoctorSignup()),
                        );
                      },
                    ),
                    SizedBox(height: 20),
                    _buildSignUpButton(
                      icon: Icons.person,
                      text: 'Sign up as a Patient',
                      onPressed: () {
                        provider.userType = 1;
                        AppRouter.router.push(DoctorSignup());
                      },
                    ),
                    SizedBox(height: 20),
                    _buildSignUpButton(
                      icon: Icons.local_hospital,
                      text: 'Sign up as a Nurse',
                      onPressed: () {
                        provider.userType = 2;
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => DoctorSignup()),
                        );
                      },
                    ),
                    SizedBox(height: 20),
                    _buildSignUpButton(
                      icon:
                          Icons.family_restroom, // Appropriate icon for mother
                      text: 'Sign up as a Mother',
                      onPressed: () {
                        provider.userType = 3;
                        Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => DoctorSignup()),
                        );
                      },
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

  Widget _buildSignUpButton({
    required IconData icon,
    required String text,
    required VoidCallback onPressed,
  }) {
    return ElevatedButton.icon(
      onPressed: onPressed,
      icon: Icon(
        icon,
        color: Colors.white,
      ),
      label: Text(
        text,
        style: TextStyle(color: Colors.white),
      ),
      style: ElevatedButton.styleFrom(
        backgroundColor: Color(0xFF199A8E),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(20.0),
        ),
        padding: EdgeInsets.symmetric(vertical: 15.0, horizontal: 20.0),
        minimumSize: Size(double.infinity, 50), // Make button fill the width
      ),
    );
  }
}

void main() {
  runApp(MaterialApp(
    home: SignupAs(),
  ));
}
