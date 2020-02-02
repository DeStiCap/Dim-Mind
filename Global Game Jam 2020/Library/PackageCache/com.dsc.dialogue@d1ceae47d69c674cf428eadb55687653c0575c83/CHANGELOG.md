## [0.1.4-preview.16] - 2019-01-30
- Event load scene now has option load async.
- Add dialogue UnityEvent controller.
- Add dialogue event to set and start new dialogue.
- Improve event choice to can choose to run on start or on end.
- Improve destroy on load performance by remove OnDestroy method from dialogue group controller.

## [0.1.3-preview.9] - 2019-01-29
- Add typing controller.
- Add Set typing event.
- Add event delay.
- Add event end.
- Add Choice system.
- Load scene event now support load by name.

## [0.1.2-preview.6] - 2019-01-28
- Update support DSC UI to V0.0.2-preview.1
- Remove canvas set event.
- Add BaseReplaceData for use replace word instead old set manual data.
- Add script template for BaseReplaceData.

## [0.1.1-preview.2] - 2019-01-27
- Change namespace from DialogueSystem to Dialogue.
- Add Pre processing for Dialogue.
- Replace controller now is derived from pre processing.
- Add audio source group controller.
- Add audio source set event.

## [0.1.0-preview.1] - 2019-01-21
- Support UI group ID for all dialogue event.

## [0.0.23-preview.1] - 2019-01-07
- Add text area attribute to struct dialogue.

## [0.0.22-preview.2] - 2019-12-23
- Change Event data group controller to use only one generic script. (Remove all non generic data group controller script.)
- Remove Dialogue UI Controller. Use Base UI Controller from UI package instead.
- Remove Dialogue UI Group Controller. Use UI Group Controller from UI package instead.
- Remove Dialogue Button. Use DSC_UI_ButtonController from UI package instead.

## [0.0.21-preview.5] - 2019-12-17
- Remove custom attribute. Use Core package attribute instead.
- Remove FlagUtility. Use Core package Utility instead.

## [0.0.20-preview.3] - 2019-12-14
- Change image group to derived from UI group.
- Change canvas group to derived from UI group.
- Add raw image group. (Move out from inside image group.)

## [0.0.20-preview.2] - 2019-12-14
- Add canvas group feature to enable/disable button.

## [0.0.20-preview.1] - 2019-12-14
- Add group event for group many event to one event for run.

## [0.0.19-preview.1] - 2019-12-13
- Fix replace color word only change color only one word in sentense that has more replace than one.
- Add repository to package json.

## [0.0.18] - 2019-12-13
- Add standalone controller for run dialogue event.
- Add canvas controller.
- Add canvas group controller.

## [0.0.17] - 2019-12-12
- Add GameObject group controller.
- Add function set new dialogue data.

## [0.0.16] - 2019-12-12
- Add event rotate image/raw image.
- Add event scene load.

## [0.0.15] - 2019-12-12
- Move show/hide event in image/raw image to news script.
- Add EnumMask attribute for see enum flag in inspector.
- Add utility for flag. Now can check flag by use this.
- Change image/raw image set type to use flag instead.

## [0.0.14] - 2019-12-11
- Remove event image, raw image, text color array.
- Change event image and raw image to image set and raw image set.

## [0.0.13] - 2019-12-11
- Add event set text align.
- Add event set image/raw image color.

## [0.0.12] - 2019-12-10
- Fix bug not invoke dialogue change event.

## [0.0.11] - 2019-12-10
- Now can ignore change color in specific type.

## [0.0.10] - 2019-12-10
- Support replace talker dialogue.
- Replace now can choose ignore type.

## [0.0.9] - 2019-12-10
- Change attribute folder name to custom attribute.

## [0.0.8] - 2019-12-10
- Fix error attribute folder meta file.

## [0.0.7] - 2019-12-10
- Add replace specific word system.
- Can change color specific word.
- Add attribute color html.

## [0.0.6] - 2019-12-09
- Add raw image controller.
- Now can set image size.

## [0.0.5] - 2019-12-09
- Remove image controller index and use image group array index instead.
- Add event text color.

## [0.0.4] - 2019-12-09
- Add dialogue event system.
- Add event sprite set image and position system.

## [0.0.3] - 2019-12-08
- Change send string data on dialogue start,change to new struct data.
- Add talker name data.

## [0.0.2] - 2019-12-08
- Fix Readme meta file.

## [0.0.1] - 2019-12-08
- Fix license meta file.

## [0.0.0] - 2019-12-07
- Add basic dialogue system.