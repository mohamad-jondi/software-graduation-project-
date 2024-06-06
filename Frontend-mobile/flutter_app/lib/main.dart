import 'package:flutter/material.dart';
import 'package:flutter_app/DoctorHomePage.dart';
import 'package:flutter_app/DoctorSignup.dart';
import 'package:flutter_app/NewPassword.dart';

class NewPassword extends StatefulWidget {
  @override
  _ResetPasswordPageState createState() => _ResetPasswordPageState();
}

class _ResetPasswordPageState extends State<NewPassword> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _confirmPasswordController =
      TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        backgroundColor: Colors.white,
        elevation: 0,
        automaticallyImplyLeading: false,
      ),
      body: Padding(
        padding: const EdgeInsets.all(20.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Create New Password',
                style: TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                  color: Colors.black,
                ),
              ),
              SizedBox(height: 10),
              Text(
                'Create your new password to login',
                style: TextStyle(
                  fontSize: 16,
                  color: Colors.grey,
                ),
              ),
              SizedBox(height: 30),
              _buildPasswordField(
                labelText: 'New Password',
                controller: _passwordController,
              ),
              SizedBox(height: 20),
              _buildPasswordField(
                labelText: 'Confirm Password',
                controller: _confirmPasswordController,
              ),
              SizedBox(height: 30),
              _buildCreatePasswordButton(),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildPasswordField({
    required String labelText,
    required TextEditingController controller,
  }) {
    return TextFormField(
      controller: controller,
      obscureText: true,
      decoration: InputDecoration(
        labelText: labelText,
        prefixIcon: Icon(Icons.lock, color: Colors.grey),
        filled: true,
        fillColor: Colors.grey[200],
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(30.0),
          borderSide: BorderSide.none,
        ),
      ),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Please enter $labelText';
        }
        return null;
      },
    );
  }

  Widget _buildCreatePasswordButton() {
    return ElevatedButton(
      onPressed: () {
        if (_formKey.currentState!.validate()) {
          if (_passwordController.text != _confirmPasswordController.text) {
            ScaffoldMessenger.of(context).showSnackBar(
              SnackBar(
                content: Text('Passwords do not match!'),
                backgroundColor: Colors.red,
              ),
            );
          } else {
            // Passwords match, proceed with your logic
          }
        }
      },
      style: ElevatedButton.styleFrom(
        backgroundColor: Color(0xFF199A8E),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(30.0),
        ),
        padding: EdgeInsets.symmetric(vertical: 15.0),
        minimumSize: Size(double.infinity, 50),
      ),
      child: Text(
        'Create Password',
        style: TextStyle(
          color: Colors.white,
          fontSize: 18,
        ),
      ),
    );
  }
}

void main() {
  runApp(MaterialApp(
    home: DoctorHomePage(),
  ));
}
