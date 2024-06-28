import 'package:flutter/material.dart';
import 'package:flutter_app/App_Router/App_Router.dart';
import 'package:flutter_app/LoginAndSignup.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:provider/provider.dart';
import 'Wellcome.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (context) => AppProvider()),
      ],
      child: MaterialApp(
        debugShowCheckedModeBanner: false,
        navigatorKey: AppRouter.router.navigatorKey,
        home: WellcomeScreen(),
      ),
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
        MaterialPageRoute(builder: (context) => LoginAndSignup()),
      );
    });
  }

  @override
  Widget build(BuildContext context) {
    return Wellcome(); // This is your welcome page
  }
}
