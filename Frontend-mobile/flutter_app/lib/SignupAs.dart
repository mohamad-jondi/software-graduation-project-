import 'package:flutter/material.dart';

class SignupAs extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
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
                      // Doctor sign up logic
                    },
                  ),
                  SizedBox(height: 20),
                  _buildSignUpButton(
                    icon: Icons.person,
                    text: 'Sign up as a Patient',
                    onPressed: () {
                      // Patient sign up logic
                    },
                  ),
                  SizedBox(height: 20),
                  _buildSignUpButton(
                    icon: Icons.local_hospital,
                    text: 'Sign up as a Nurse',
                    onPressed: () {
                      // Nurse sign up logic
                    },
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
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
