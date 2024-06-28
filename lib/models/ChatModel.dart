import 'dart:convert';

import 'Message.dart';

class Chat {
  int? chatId;
  String? firstPartyUserName;
  String? secondPartyUsername;
  bool? isTheLastSenderMe;
  String? lastSentMassagess;
  int? numberOfMessages;
  List<Message>? messages;
  DateTime? lastMessageDate;
  bool messagesFetched;
  Chat(
      {this.chatId,
      this.firstPartyUserName,
      this.secondPartyUsername,
      this.isTheLastSenderMe,
      this.lastSentMassagess,
      this.numberOfMessages,
      this.messagesFetched = false,
      this.messages,
      this.lastMessageDate});
  fillMessages(List data) {
    messages = data.map((e) => Message.fromMap(e)).toList();
    messagesFetched = true;
  }

  Map<String, dynamic> toMap() {
    return {
      'chatId': chatId,
      'firstPartyUserName': firstPartyUserName,
      'secondPartyUsername': secondPartyUsername,
      'isTheLastSenderMe': isTheLastSenderMe,
      'lastSentMassagess': lastSentMassagess,
      'numberOfMessages': numberOfMessages,
      'messagesFetched': messagesFetched,
    };
  }

  factory Chat.fromMap(Map<String, dynamic> map) {
    return Chat(
      chatId: map['chatId']?.toInt(),
      firstPartyUserName: map['firstPartyUserName'],
      secondPartyUsername: map['secondPartyUsername'],
      isTheLastSenderMe: map['isTheLastSenderMe'],
      lastSentMassagess: map['lastSentMassagess'],
      numberOfMessages: map['numberOfMessages']?.toInt(),
      messagesFetched: map['messagesFetched'] ?? false,
      lastMessageDate: map['lastMessageDate'] != null
          ? DateTime.parse(map['lastMessageDate'])
          : DateTime.now(),
    );
  }

  String toJson() => json.encode(toMap());

  factory Chat.fromJson(String source) => Chat.fromMap(json.decode(source));
}
