import 'dart:convert';

class Message {
  int? chatMessageID;
  String? senderUsername;
  String? sentDateTime;
  String? messageContent;
  bool? isDeleted;
  bool? isRead;
  Message({
    this.chatMessageID,
    this.senderUsername,
    this.sentDateTime,
    this.messageContent,
    this.isDeleted,
    this.isRead,
  });

  Map<String, dynamic> toMap() {
    return {
      'chatMessageID': chatMessageID,
      'senderUsername': senderUsername,
      'sentDateTime': sentDateTime,
      'messageContent': messageContent,
      'isDeleted': isDeleted,
      'isRead': isRead,
    };
  }

  factory Message.fromMap(Map<String, dynamic> map) {
    return Message(
      chatMessageID: map['chatMessageID']?.toInt(),
      senderUsername: map['senderUsername'],
      sentDateTime: map['sentDateTime'],
      messageContent: map['messageContent'],
      isDeleted: map['isDeleted'],
      isRead: map['isRead'],
    );
  }
  

  String toJson() => json.encode(toMap());

  factory Message.fromJson(String source) => Message.fromMap(json.decode(source));
}
