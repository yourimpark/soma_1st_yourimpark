﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VoxelBusters.NativePlugins
{
	/// <summary>
	/// Used for scheduling, registering and handling both local and remote notifications.
	/// </summary>
	public partial class NotificationService : MonoBehaviour 
	{
		#region properties

		protected NotificationType		m_supportedNotifcationTypes		= (NotificationType)0;

		#endregion

		#region Unity Methods

		private void Awake ()
		{
			if(NPSettings.Application.SupportedFeatures.UsesNotificationService)
			{
				// Initialise component
				Initialise(NPSettings.Notification);
			}
		}

		#endregion

		#region Initialise

		protected virtual void Initialise (NotificationServiceSettings _settings)
		{}

		#endregion

		#region Local Notification API's

		/// <summary>
		/// Apps that use either local or remote notifications must register the types of notifications they intend to deliver.
		/// </summary>
		/// <param name="_notificationTypes">The notification types that your app supports.</param>
		/// <description>
		/// If your app displays alerts, play sounds, or badges its icon, you must call this method during your launch.
		/// </description>
		public virtual void RegisterNotificationTypes (NotificationType _notificationTypes)
		{
			m_supportedNotifcationTypes	= _notificationTypes;
		}

		/// <summary>
		/// Schedules a local notification.
		/// </summary>
		/// <returns>Notification ID that can be used to uniquely identify every scheduled notification.</returns>
		/// <param name="_notification">Notification to be scheduled.</param>
		public virtual string ScheduleLocalNotification (CrossPlatformNotification _notification)
		{
			return null;
		}

		/// <summary>
		/// Cancels the delivery of the specified scheduled local notification.
		/// </summary>
		/// <param name="_notificationID">Identifier for the scheduled notification to be cancelled.</param>
		public virtual void CancelLocalNotification (string _notificationID)
		{}

		/// <summary>
		/// Cancels the delivery of all scheduled local notification.
		/// </summary>
		public virtual void CancelAllLocalNotification ()
		{}
			
		/// <summary>
		/// Discards all received notifications.
		/// </summary>
		public virtual void ClearNotifications ()
		{}
		
		#endregion

		#region Remote Notification API's

		/// <summary>
		/// Register to receive remote notifications via Push Notification service.
		/// </summary>
		public virtual void RegisterForRemoteNotifications ()
		{}

		/// <summary>
		/// Unregister for all remote notifications received via Push Notification service.
		/// </summary>
		public virtual void UnregisterForRemoteNotifications ()
		{}

		#endregion
	}
}