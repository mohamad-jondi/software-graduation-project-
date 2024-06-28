import 'package:flutter/material.dart';

class Wellcome extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        color: Color(0xFF199A8E), // Background color
        child: Center(
          child: Image.asset(
            'images/AppLogo.png',
          ),
        ),
      ),
    );
  }
}
