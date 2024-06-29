import 'dart:convert';

class Drug {
   String? name;
  String? duration;
  int? quantityConsumed;
  int? drugDosageTime;
  Drug({
    this.name,
    this.duration,
    this.quantityConsumed,
    this.drugDosageTime,
  });

  Map<String, dynamic> toMap() {
    return {
      'name': name,
      'duration': duration,
      'quantityConsumed': quantityConsumed,
      'drugDosageTime': drugDosageTime,
    };
  }

  factory Drug.fromMap(Map<String, dynamic> map) {
    return Drug(
      name: map['name'],
      duration: map['duration'],
      quantityConsumed: map['quantityConsumed']?.toInt(),
      drugDosageTime: map['drugDosageTime']?.toInt(),
    );
  }

  String toJson() => json.encode(toMap());

  factory Drug.fromJson(String source) => Drug.fromMap(json.decode(source));
}
