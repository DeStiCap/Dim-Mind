## [0.1.11-preview.1] -2019-01-31
- Improve GameObject to have method destroy itself.

## [0.1.10-preview.11] -2019-01-30
- Add event run by time GameObject.
- Add event Add explosion force.
- Improve load scene event to have async load.
- Improve event run by overlap, raycast, linecast to show gizmos during select.
- Improve add force event to support select ForceMode in inspector.
- Change event run by button to disable run event when button component is disable.
- Change event run by overlap, raycast to can set max send data instead send only one time option.
- Fix bug index error in event run by get key.

## [0.1.9-preview.2] -2019-01-29
- Change name run event by button to input button.
- Add run event by button. (Button UI)

## [0.1.8-preview.2] -2019-01-28
- Add event run by video player.
- Improve event load scene to support load previous/next scene by current scene index.

## [0.1.7-preview.3] -2019-01-26
- Improve event run by trigger to support send GameObject data.
- Improve event run by collision to support send GameObject data.
- Add event transform.
- Add event rigidbody.

## [0.1.6-preview.6] -2019-01-26
- Add Event Condition script template.
- Add event run by button. (New input.)
- Add event run by raycast.
- Add event run by linecast.
- Improve event run by overlap to send GameObject data.

## [0.1.5-preview.2] -2019-01-25
- Add event time for set time scale.
- Add run event by event GameObject.
- Improve event add force to support add force to another GameObject.

## [0.1.4-preview.2] -2019-01-24
- Change DSC_Event_DestroyGameObject event to DSC_Event_GameObject.
- Add event instantiate GameObject.

## [0.1.3-preview.1] -2019-01-23
- Add event run by overlap circle.
- Add event run by overlap box2D.
- Add event run by overlap box.
- Add event run by overlap sphere.

## [0.1.2-preview.2] -2019-01-22
- Change package name from GameEventSystem to event.

## [0.1.1-preview.2] -2019-01-21
- Add helper event run by time.
- Change new helper event namespace to DSC.GameEventSystem.HelperEvents for the same as old one.
- Change condition data in inspector to alway place over event data.
- Fix some helper event not create condition data.

## [0.1.0-preview.1] -2019-01-06
- Add event condition data.
- Add helper event check condition.
- All run helper event now support condition.

## [0.0.11-preview.2] -2019-01-04
- Add helper event string.
- Add helper event int.
- Add helper event float.
- Add helper event bool.
- Improve helper event run event by collision to support send event data.
- Improve helper event run event by collision2D to support send event data.
- Improve helper event run event by trigger to support send event data.
- Improve helper event run event by trigger2D to support send event data.

## [0.0.10-preview.3] -2019-01-04
- Improve event run by get key. Now can has array of event to call when press same button.

## [0.0.9-preview.1] -2019-12-25
- Add script event set for set group of event to other event.
- Improve event run by event to can change set events.

## [0.0.8-preview.1] -2019-12-17
- Add event run by collision.
- Add event run by event.

## [0.0.7] -2019-12-08
- Add helper event for mono function.

## [0.0.6] -2019-11-23
- Fix meta files error.

## [0.0.5] - 2019-10-24
- Add script event run other event by trigger.
- Add script event run other event by trigger 2D.
- Add script event destroy game object.

## [0.0.4] - 2019-10-23
- Add script event run other event by get key.
- Add script event add force.
- Add script event add force 2d.
- Move helper events to new folder.
. Change helper events script namespace.

## [0.0.3] - 2019-10-22
- Add script event quit application.
- Change namespace.

## [0.0.2] - 2019-10-22
- Add script event load scene.
- Change all GameEventCallback function name.

## [0.0.1] - 2019-10-20
- Now GameEventCallback can receive 3 arguments.

## [0.0.0] - 2019-10-20
- Add Event Callback system.