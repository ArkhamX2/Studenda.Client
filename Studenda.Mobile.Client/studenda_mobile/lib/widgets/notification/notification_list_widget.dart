import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/common/notification.dart';
import 'package:studenda_mobile/widgets/notification/notification_item_widget.dart';

class NotificationListWidget extends StatelessWidget {
  final List<StudendaNotification> notifications;

  const NotificationListWidget({super.key, required this.notifications});

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      children: notifications
          .map((element) => NotificationItemWidget(notification: element))
          .toList(),
    );
  }
}
