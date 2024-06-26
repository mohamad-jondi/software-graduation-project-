import 'package:flutter/material.dart';

class SetAvailabilityPage extends StatefulWidget {
  @override
  _SetAvailabilityPageState createState() => _SetAvailabilityPageState();
}

class _SetAvailabilityPageState extends State<SetAvailabilityPage> {
  final Map<String, TimeOfDay> _startTimes = {};
  final Map<String, TimeOfDay> _endTimes = {};

  final List<String> daysOfWeek = [
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
    'Sunday'
  ];

  @override
  void initState() {
    super.initState();
    for (String day in daysOfWeek) {
      _startTimes[day] = TimeOfDay(hour: 9, minute: 0);
      _endTimes[day] = TimeOfDay(hour: 17, minute: 0);
    }
  }

  Future<void> _selectTime(
      BuildContext context, String day, bool isStartTime) async {
    final TimeOfDay? picked = await showTimePicker(
      context: context,
      initialTime: isStartTime ? _startTimes[day]! : _endTimes[day]!,
    );
    if (picked != null &&
        picked != (isStartTime ? _startTimes[day] : _endTimes[day])) {
      setState(() {
        if (isStartTime) {
          _startTimes[day] = picked;
        } else {
          _endTimes[day] = picked;
        }
      });
    }
  }

  void _saveAvailability() {
    // Here you can save the availability to the database or API
    print('Availability saved:');
    for (String day in daysOfWeek) {
      print(
          '$day: ${_startTimes[day]!.format(context)} - ${_endTimes[day]!.format(context)}');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Set Availability', style: TextStyle(color: Colors.white)),
        iconTheme: IconThemeData(color: Colors.white),
        backgroundColor: Color(0xFF199A8E),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: ListView(
          children: daysOfWeek.map((day) {
            return Padding(
              padding: const EdgeInsets.symmetric(vertical: 8.0),
              child: Card(
                child: ListTile(
                  title: Text(day),
                  subtitle: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      TextButton(
                        onPressed: () => _selectTime(context, day, true),
                        child:
                            Text('Start: ${_startTimes[day]!.format(context)}'),
                      ),
                      TextButton(
                        onPressed: () => _selectTime(context, day, false),
                        child: Text('End: ${_endTimes[day]!.format(context)}'),
                      ),
                    ],
                  ),
                ),
              ),
            );
          }).toList(),
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _saveAvailability,
        child: Icon(Icons.save, color: Colors.white),
        backgroundColor: Color(0xFF199A8E),
      ),
    );
  }
}
