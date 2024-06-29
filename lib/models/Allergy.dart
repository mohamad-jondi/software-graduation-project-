import 'dart:convert';

class Allergy {
  String? allergy;
  String? reactionDescription;
  String? severity;
  Allergy({
    this.allergy,
    this.reactionDescription,
    this.severity,
  });

  Map<String, dynamic> toMap() {
    return {
      'allergey': allergy,
      'reactionDescription': reactionDescription,
      'severity': severity,
    };
  }

  factory Allergy.fromMap(Map<String, dynamic> map) {
    return Allergy(
      allergy: map['allergey'],
      reactionDescription: map['reactionDescription'],
      severity: map['severity'],
    );
  }

  String toJson() => json.encode(toMap());

  factory Allergy.fromJson(String source) =>
      Allergy.fromMap(json.decode(source));
}
