import 'package:flutter/material.dart';

class VerificationPage extends StatefulWidget {
  @override
  _EnterVerificationCodeState createState() => _EnterVerificationCodeState();
}

class _EnterVerificationCodeState extends State<VerificationPage> {
  final List<TextEditingController> _controllers =
      List.generate(6, (_) => TextEditingController());
  final FocusNode _focusNode = FocusNode();

  @override
  void initState() {
    super.initState();
    _controllers.forEach((controller) {
      controller.addListener(_onTextChanged);
    });
  }

  @override
  void dispose() {
    _controllers.forEach((controller) {
      controller.dispose();
    });
    _focusNode.dispose();
    super.dispose();
  }

  void _onTextChanged() {
    for (var i = 0; i < _controllers.length - 1; i++) {
      if (_controllers[i].text.length == 1) {
        FocusScope.of(context).nextFocus();
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final GlobalKey<FormState> _formKey = GlobalKey<FormState>();

    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        backgroundColor: Colors.white,
        elevation: 0,
        automaticallyImplyLeading: false,
      ),
      body: Padding(
        padding: const EdgeInsets.all(20.0),
        child: Form(
          key: _formKey,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Enter Verification Code',
                style: TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                  color: Colors.black,
                ),
              ),
              SizedBox(height: 20),
              Text(
                'Enter the code that we have sent to your email',
                style: TextStyle(
                  fontSize: 16,
                  color: Colors.black,
                ),
              ),
              SizedBox(height: 30),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: List.generate(6, (index) => _buildCodeField(index)),
              ),
              SizedBox(height: 30),
              ElevatedButton(
                onPressed: () {
                  if (_formKey.currentState!.validate()) {
                    // Verification logic
                  }
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Color(0xFF199A8E),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(30.0),
                  ),
                  padding:
                      EdgeInsets.symmetric(vertical: 15.0, horizontal: 20.0),
                  minimumSize: Size(double.infinity, 50),
                ),
                child: Text(
                  'Verify',
                  style: TextStyle(
                    color: Colors.white,
                    fontSize: 18,
                  ),
                ),
              ),
              SizedBox(height: 20),
              Center(
                child: Text.rich(
                  TextSpan(
                    text: 'Didn’t receive the code? ',
                    style: TextStyle(color: Colors.black),
                    children: [
                      TextSpan(
                        text: 'Resend',
                        style: TextStyle(color: Color(0xFF199A8E)),
                        // Add resend logic
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildCodeField(int index) {
    return Container(
      width: 40,
      child: TextFormField(
        controller: _controllers[index],
        focusNode:
            index == 0 ? _focusNode : null, // Only the first field gets focus
        decoration: InputDecoration(
          filled: true,
          fillColor: Colors.grey[200],
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
            borderSide: BorderSide.none,
          ),
        ),
        textAlign: TextAlign.center,
        keyboardType: TextInputType.text,
        maxLength: 1,
        onChanged: (value) {
          if (value.isNotEmpty) {
            // Automatically move focus to the next field
            if (index < _controllers.length - 1) {
              FocusScope.of(context).nextFocus();
            }
          }
        },
        validator: (value) {
          if (value == null || value.isEmpty) {
            return 'Please fill in all fields';
          }
          return null;
        },
      ),
    );
  }
}

void main() {
  runApp(MaterialApp(
    home: VerificationPage(),
  ));
}
