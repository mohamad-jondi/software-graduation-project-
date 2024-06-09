import 'package:flutter/material.dart';
import 'package:flutter_app/PatientHomePage.dart';
import 'package:flutter_app/ResetPassword.dart';
import 'package:flutter_app/SignupAs.dart';

class SignInPage extends StatefulWidget {
  @override
  _SignInPageState createState() => _SignInPageState();
}

class _SignInPageState extends State<SignInPage> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  String _email = '';
  String _password = '';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        title: Center(
          child: Text('Login', style: TextStyle(color: Colors.black)),
        ),
        backgroundColor: Colors.white,
      ),
      body: Padding(
        padding: const EdgeInsets.all(20.0),
        child: Form(
          key: _formKey,
          child: ListView(
            children: [
              SizedBox(height: 20),
              _buildEmailTextField(),
              SizedBox(height: 20),
              _buildPasswordTextField(),
              SizedBox(height: 20),
              ElevatedButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => PatientHomePage()),
                  );
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Color(0xFF199A8E), // Background color
                  padding:
                      EdgeInsets.symmetric(vertical: 15.0), // Increase height
                ),
                child: Text(
                  'Login',
                  style: TextStyle(
                    color: Colors.white,
                  ),
                ),
              ),
              SizedBox(height: 10),
              TextButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => ResetPassword()),
                  );
                },
                style: ButtonStyle(
                  foregroundColor:
                      MaterialStateProperty.all<Color>(Colors.black),
                ),
                child: Text(
                  'Forgot Password?',
                  style: TextStyle(color: Colors.blue),
                ),
              ),
              SizedBox(height: 10),
              TextButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => SignupAs()),
                  );
                },
                style: ButtonStyle(
                  foregroundColor:
                      MaterialStateProperty.all<Color>(Colors.black),
                ),
                child: Text(
                  "Don't have an account? Sign Up",
                  style: TextStyle(color: Colors.blue),
                ),
              ),
              SizedBox(height: 20),
              Divider(),
              SizedBox(height: 20),
              Text(
                'Or sign in with',
                textAlign: TextAlign.center,
                style: TextStyle(color: Colors.black),
              ),
              SizedBox(height: 20),
              Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  SizedBox(height: 10),
                  SizedBox(
                    width: double.infinity,
                    child: _buildSocialButton(
                        Icons.golf_course, 'Sign in with Google', Colors.red,
                        () {
                      // Google login logic
                    }),
                  ),
                  SizedBox(height: 20),
                  SizedBox(
                    width: double.infinity,
                    child: _buildSocialButton(
                        Icons.apple, 'Sign in with Apple', Colors.grey, () {
                      // Apple login logic
                    }),
                  ),
                  SizedBox(height: 20),
                  SizedBox(
                    width: double.infinity,
                    child: _buildSocialButton(
                        Icons.facebook, 'Sign in with Facebook', Colors.blue,
                        () {
                      // Facebook login logic
                    }),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildEmailTextField() {
    return TextFormField(
      controller: _emailController,
      decoration: InputDecoration(
        labelText: 'Enter your email',
        prefixIcon: Icon(Icons.email),
        filled: true,
        fillColor: Colors.grey[200],
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(30.0), // Rounded corners
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(30.0),
          borderSide: BorderSide(color: Colors.black),
        ),
      ),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Please enter your email';
        }
        return null;
      },
      onChanged: (value) {
        setState(() {
          _email = value;
        });
      },
    );
  }

  Widget _buildPasswordTextField() {
    return TextFormField(
      controller: _passwordController,
      decoration: InputDecoration(
        labelText: 'Enter your password',
        prefixIcon: Icon(Icons.lock),
        filled: true,
        fillColor: Colors.grey[200],
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(30.0), // Rounded corners
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(30.0),
          borderSide: BorderSide(color: Colors.black),
        ),
      ),
      obscureText: true,
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Please enter your password';
        }
        return null;
      },
      onChanged: (value) {
        setState(() {
          _password = value;
        });
      },
    );
  }

  void _submitForm() {
    if (_formKey.currentState!.validate()) {
      // Submit form logic
    }
  }

  Widget _buildSocialButton(
      IconData icon, String text, Color iconColor, VoidCallback onPressed) {
    return ElevatedButton.icon(
      onPressed: onPressed,
      icon: Icon(
        icon,
        color: iconColor,
      ),
      label: Text(
        text,
        style: TextStyle(color: Colors.black), // Text color
      ),
      style: ElevatedButton.styleFrom(
        backgroundColor: Colors.white, // Button background color
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(30.0),
          side: BorderSide(color: Colors.black), // Black border
        ),
        padding: EdgeInsets.symmetric(
            vertical: 20.0, horizontal: 20.0), // Increase size
      ),
    );
  }
}

void main() {
  runApp(MaterialApp(
    home: SignInPage(),
  ));
}
