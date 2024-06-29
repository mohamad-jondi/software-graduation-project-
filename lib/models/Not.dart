import 'dart:convert';

class Not {
  String? notificationContent;
  int? notificationType;
  bool? isRead;
  DateTime date;
  Not({
    this.notificationContent,
    this.notificationType,
    this.isRead,
    required this.date
  });

  Map<String, dynamic> toMap() {
    return {
      'notificationContent': notificationContent,
      'notificationType': notificationType,
      'isRead': isRead,
      'date': date.toString()
    };
  }

  factory Not.fromMap(Map<String, dynamic> map) {
    return Not(
      notificationContent: map['notificationContent'],
      notificationType: map['notificationType']?.toInt(),
      isRead: map['isRead'],
      date: DateTime.parse(map['date']?? DateTime.now().toString())
    );
  }

  String toJson() => json.encode(toMap());

  factory Not.fromJson(String source) => Not.fromMap(json.decode(source));
}
