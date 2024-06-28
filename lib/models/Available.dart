import 'dart:convert';

class Available {
  int? dayOfWeek;
  String? startHour;
  String? endHour;
  Available({
    this.dayOfWeek,
    this.startHour,
    this.endHour,
  });

  Map<String, dynamic> toMap() {
    return {
      'dayOfWeek': dayOfWeek,
      'startHour': startHour,
      'endHour': endHour,
    };
  }

  factory Available.fromMap(Map<String, dynamic> map) {
    return Available(
      dayOfWeek: map['dayOfWeek']?.toInt(),
      startHour: map['startHour'],
      endHour: map['endHour'],
    );
  }

  String toJson() => json.encode(toMap());

  factory Available.fromJson(String source) => Available.fromMap(json.decode(source));
}
