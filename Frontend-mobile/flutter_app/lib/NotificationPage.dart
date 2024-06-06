import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class NotificationPage extends StatefulWidget {
  @override
  _NotificationPageState createState() => _NotificationPageState();
}

class _NotificationPageState extends State<NotificationPage> {
  final List<Map<String, dynamic>> notifications = [
    {
      "title": "Appointment Reminder",
      "body": "You have an appointment with John Doe tomorrow at 10:00 AM.",
      "date": DateTime.now(),
      "isRead": false
    },
    {
      "title": "New Message",
      "body": "You have a new message from Jane Smith.",
      "date": DateTime.now().subtract(Duration(days: 1)),
      "isRead": false
    },
    {
      "title": "Profile Update",
      "body": "Your profile information has been updated successfully.",
      "date": DateTime.now().subtract(Duration(days: 7)),
      "isRead": false
    },
  ];

  void _markAsRead(int index) {
    setState(() {
      notifications[index]['isRead'] = true;
    });
  }

  List<Map<String, dynamic>> _getNotificationsByPeriod(
      DateTime start, DateTime end) {
    return notifications.where((notification) {
      final date = notification['date'];
      if (date == null) return false;
      return date.isAfter(start) && date.isBefore(end);
    }).toList();
  }

  @override
  Widget build(BuildContext context) {
    DateTime now = DateTime.now();
    DateTime startOfToday = DateTime(now.year, now.month, now.day);
    DateTime startOfLastWeek =
        startOfToday.subtract(Duration(days: now.weekday));
    DateTime startOfLastMonth = DateTime(now.year, now.month - 1);

    List<Map<String, dynamic>> todayNotifications =
        _getNotificationsByPeriod(startOfToday, now);
    List<Map<String, dynamic>> lastWeekNotifications =
        _getNotificationsByPeriod(startOfLastWeek, startOfToday);
    List<Map<String, dynamic>> lastMonthNotifications =
        _getNotificationsByPeriod(startOfLastMonth, startOfLastWeek);

    return Scaffold(
      body: ListView(
        children: [
          if (todayNotifications.isNotEmpty) ...[
            _buildSectionTitle('Today:'),
            Divider(),
            ...todayNotifications
                .map((notification) => _buildNotificationCard(notification))
                .toList(),
          ],
          if (lastWeekNotifications.isNotEmpty) ...[
            _buildSectionTitle('Last Week:'),
            Divider(),
            ...lastWeekNotifications
                .map((notification) => _buildNotificationCard(notification))
                .toList(),
          ],
          if (lastMonthNotifications.isNotEmpty) ...[
            _buildSectionTitle('Last Month:'),
            Divider(),
            ...lastMonthNotifications
                .map((notification) => _buildNotificationCard(notification))
                .toList(),
          ],
        ],
      ),
    );
  }

  Widget _buildSectionTitle(String title) {
    return Padding(
      padding: const EdgeInsets.all(16.0),
      child: Text(
        title,
        style: TextStyle(
          fontSize: 22,
          color: Colors.black,
        ),
      ),
    );
  }

  Widget _buildNotificationCard(Map<String, dynamic> notification) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 5.0, horizontal: 10.0),
      decoration: BoxDecoration(
        border: Border.all(color: Colors.grey),
        borderRadius: BorderRadius.circular(10.0),
      ),
      child: Padding(
        padding: const EdgeInsets.all(10.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              notification['title'] ?? '',
              style: TextStyle(
                fontSize: 18,
                color: Color(0xFF199A8E),
              ),
            ),
            SizedBox(height: 5),
            Text(notification['body'] ?? ''),
            SizedBox(height: 10),
            if (!notification['isRead'])
              Align(
                alignment: Alignment.centerRight,
                child: ElevatedButton(
                  onPressed: () =>
                      _markAsRead(notifications.indexOf(notification)),
                  style: ElevatedButton.styleFrom(
                    backgroundColor:
                        Color.fromARGB(255, 63, 189, 176), // Background color
                  ),
                  child: Text(
                    'Mark as Read',
                    style: TextStyle(color: Colors.white), // Text color
                  ),
                ),
              ),
            if (notification['isRead'])
              Align(
                alignment: Alignment.centerRight,
                child: Text(
                  'Read',
                  style: TextStyle(color: Colors.green),
                ),
              ),
          ],
        ),
      ),
    );
  }
}
