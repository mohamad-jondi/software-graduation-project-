import 'package:flutter/material.dart';
import 'package:flutter_app/models/Not.dart';
import 'package:flutter_app/providers/AppProvider.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

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

    return Consumer<AppProvider>(builder: (context, provider, x) {
      return Scaffold(
        body: ListView(
          children: [
            if (provider.nots
                .where((e) => e.date.day == DateTime.now().day)
                .toList()
                .isNotEmpty) ...[
              _buildSectionTitle('Today:'),
              Divider(),
              ...provider.nots
                  .where((e) => e.date.day == DateTime.now().day)
                  .toList()
                  .map((notification) => _buildNotificationCard(notification))
                  .toList(),
            ],
            if (provider.nots
                .where((e) =>
                    e.date.difference(DateTime.now()).inDays > 0 &&
                    e.date.difference(DateTime.now()).inDays <= 7)
                .toList()
                .isNotEmpty) ...[
              _buildSectionTitle('Last Week:'),
              Divider(),
              ...provider.nots
                  .where((e) =>
                      e.date.difference(DateTime.now()).inDays > 0 &&
                      e.date.difference(DateTime.now()).inDays <= 7)
                  .toList()
                  .map((notification) => _buildNotificationCard(notification))
                  .toList(),
            ],
            if (provider.nots
                .where((e) =>
                    e.date.difference(DateTime.now()).inDays > 7 &&
                    e.date.difference(DateTime.now()).inDays < 30)
                .toList()
                .isNotEmpty) ...[
              _buildSectionTitle('Last Month:'),
              Divider(),
              ...provider.nots
                  .where((e) =>
                      e.date.difference(DateTime.now()).inDays > 7 &&
                      e.date.difference(DateTime.now()).inDays < 30)
                  .toList()
                  .map((notification) => _buildNotificationCard(notification))
                  .toList(),
            ],
          ],
        ),
      );
    });
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

  Widget _buildNotificationCard(Not notification) {
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
              notification.notificationType == 0
                  ? "Appointment Reminder"
                  : 'New Message',
              style: TextStyle(
                fontSize: 18,
                color: Color(0xFF199A8E),
              ),
            ),
            SizedBox(height: 5),
            Text(notification.notificationContent ?? ''),
            SizedBox(height: 10),
            Align(
                alignment: Alignment.centerRight,
                child: Text(formatTime(notification.date))),
          ],
        ),
      ),
    );
  }
}

String formatTime(DateTime notificationTime) {
  final now = DateTime.now();
  final difference = now.difference(notificationTime);

  if (difference.inSeconds < 60) {
    return 'Now';
  } else if (difference.inMinutes < 10) {
    return '${difference.inMinutes} minutes';
  } else if (difference.inMinutes < 60) {
    return '${difference.inMinutes} minutes';
  } else if (difference.inHours < 9) {
    return '${difference.inHours} hours';
  } else if (difference.inHours < 24) {
    return '${difference.inHours} hours';
  } else {
    final days = difference.inDays;
    if (days == 1) {
      return 'yesterday';
    } else {
      return DateFormat.yMMMMd('en').format(notificationTime);
    }
  }
}
