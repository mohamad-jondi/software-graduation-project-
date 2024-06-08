import 'package:flutter/material.dart';
import 'package:flutter_app/SignInPage.dart';
import 'package:flutter_app/SignupAs.dart';

class LoginAndSignup extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        padding: EdgeInsets.symmetric(horizontal: 20.0),
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Image.asset(
                'images/Logo.png',
                height: 150,
                width: 150,
                fit: BoxFit.fill,
              ),
              const SizedBox(
                  height:
                      20), // Optional: Add some space between the image and the text
              const Text(
                "Let’s get started!",
                style: TextStyle(
                  fontSize: 24, // You can adjust the font size as needed
                  fontWeight: FontWeight.bold, // Optional: Make the text bold
                  color: Colors.black, // You can set the text color if needed
                ),
              ),
              const SizedBox(height: 20),
              const Center(
                child: Text(
                  "Login to enjoy the features we’ve provided, and stay healthy!",
                  textAlign: TextAlign.center, // Center the text
                  style: TextStyle(
                    fontSize: 18, // You can adjust the font size as needed
                    color: Colors.grey, // You can set the text color if needed
                  ),
                ),
              ),
              const SizedBox(height: 35),
              ElevatedButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => SignInPage()),
                  );
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: const Color(0xFF199A8E), // Background color
                  shape: RoundedRectangleBorder(
                    borderRadius:
                        BorderRadius.circular(30.0), // Rounded corners
                  ),
                  padding: const EdgeInsets.symmetric(
                      vertical: 20.0,
                      horizontal:
                          150.0), // Increase the padding to make the button wider and taller
                ),
                child: const Text(
                  'Log In',
                  style: TextStyle(
                    color: Colors.white, // Text color
                    fontSize: 16,
                  ),
                ),
              ),
              const SizedBox(height: 20), // Space between the two buttons
              OutlinedButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => SignupAs()),
                  );
                },
                style: OutlinedButton.styleFrom(
                  foregroundColor: const Color(0xFF199A8E), // Text color
                  side: const BorderSide(
                      color: Color(0xFF199A8E)), // Border color
                  shape: RoundedRectangleBorder(
                    borderRadius:
                        BorderRadius.circular(30.0), // Rounded corners
                  ),
                  padding: const EdgeInsets.symmetric(
                      vertical: 20.0,
                      horizontal:
                          150.0), // Increase the padding to make the button wider and taller
                ),
                child: const Text(
                  'Sign Up',
                  style: TextStyle(
                    color: Color(0xFF199A8E), // Text color
                    fontSize: 16,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
