﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubnubApi
{
    public class SubscribeCallbackExt : SubscribeCallback
    {
        readonly Action<Pubnub, PNMessageResult<object>> subscribeAction;
        readonly Action<Pubnub, PNPresenceEventResult> presenceAction;
        readonly Action<Pubnub, PNSignalResult<object>> signalAction;
        readonly Action<Pubnub, PNStatus> statusAction;
        //readonly Action<Pubnub, PNUserEventResult> userAction;
        //readonly Action<Pubnub, PNSpaceEventResult> spaceAction;
        //readonly Action<Pubnub, PNMembershipEventResult> membershipAction;
        readonly Action<Pubnub, PNObjectApiEventResult> objectApiAction;

        public SubscribeCallbackExt(Action<Pubnub, PNMessageResult<object>> messageCallback, Action<Pubnub, PNPresenceEventResult> presenceCallback, Action<Pubnub, PNStatus> statusCallback)
        {
            this.subscribeAction = messageCallback;
            this.presenceAction = presenceCallback;
            this.statusAction = statusCallback;
            this.signalAction = null;
            //this.userAction = null;
            //this.spaceAction = null;
            //this.membershipAction = null;
            this.objectApiAction = null;
        }

        public SubscribeCallbackExt(Action<Pubnub, PNSignalResult<object>> signalCallback, Action<Pubnub, PNStatus> statusCallback)
        {
            this.subscribeAction = null;
            this.presenceAction = null;
            //this.userAction = null;
            //this.spaceAction = null;
            //this.membershipAction = null;
            this.statusAction = statusCallback;
            this.signalAction = signalCallback;
            this.objectApiAction = null;
        }

        //public SubscribeCallbackExt(Action<Pubnub, PNUserEventResult> userCallback, Action<Pubnub, PNSpaceEventResult> spaceCallback, Action<Pubnub, PNMembershipEventResult> membershipCallback, Action<Pubnub, PNStatus> statusCallback)
        //{
        //    this.subscribeAction = null;
        //    this.presenceAction = null;
        //    this.signalAction = null;
        //    this.statusAction = statusCallback;
        //    this.userAction = userCallback;
        //    this.spaceAction = spaceCallback;
        //    this.membershipAction = membershipCallback;
        //    this.objectApiAction = null;

        //}

        public SubscribeCallbackExt(Action<Pubnub, PNObjectApiEventResult> objectApiCallback, Action<Pubnub, PNStatus> statusCallback)
        {
            this.subscribeAction = null;
            this.presenceAction = null;
            this.signalAction = null;
            this.statusAction = statusCallback;
            //this.userAction = null;
            //this.spaceAction = null;
            //this.membershipAction = null;
            this.objectApiAction = objectApiCallback;

        }

        public override void Message<T>(Pubnub pubnub, PNMessageResult<T> message)
        {
            PNMessageResult<object> message1 = new PNMessageResult<object>();
            message1.Channel = message.Channel;
            message1.Message = (T)(object)message.Message;
            message1.Subscription = message.Subscription;
            message1.Timetoken = message.Timetoken;
            message1.UserMetadata = message.UserMetadata;
            message1.Publisher = message.Publisher;

            subscribeAction?.Invoke(pubnub, message1);
        }

        public override void Presence(Pubnub pubnub, PNPresenceEventResult presence)
        {
            presenceAction?.Invoke(pubnub, presence);
        }

        public override void Status(Pubnub pubnub, PNStatus status)
        {
            statusAction?.Invoke(pubnub, status);
        }

        public override void Signal<T>(Pubnub pubnub, PNSignalResult<T> signalMessage)
        {
            PNSignalResult<object> message1 = new PNSignalResult<object>();
            message1.Channel = signalMessage.Channel;
            message1.Message = (T)(object)signalMessage.Message;
            message1.Subscription = signalMessage.Subscription;
            message1.Timetoken = signalMessage.Timetoken;
            message1.UserMetadata = signalMessage.UserMetadata;
            message1.Publisher = signalMessage.Publisher;

            signalAction?.Invoke(pubnub, message1);
        }

        //public override void User(Pubnub pubnub, PNUserEventResult userEvent)
        //{
        //    userAction?.Invoke(pubnub, userEvent);
        //}

        //public override void Space(Pubnub pubnub, PNSpaceEventResult spaceEvent)
        //{
        //    spaceAction?.Invoke(pubnub, spaceEvent);
        //}

        //public override void Membership(Pubnub pubnub, PNMembershipEventResult membershipEvent)
        //{
        //    membershipAction?.Invoke(pubnub, membershipEvent);
        //}

        public override void ObjectEvent(Pubnub pubnub, PNObjectApiEventResult objectEvent)
        {
            objectApiAction?.Invoke(pubnub, objectEvent);
        }
    }
}
