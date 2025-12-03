# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]




### Added

- `(I)LinkMobilityClient` with client options to communicate with the REST API
- Methods to send SMS messages
  - `SendTextMessage()` sending a normal text message
  - `SendBinaryMessage()` sending a binary message as _base64_ encoded message segments
- `IncomingMessageNotification` to support receiving incoming messages or delivery reports via WebHooks
- UnitTesting



[Unreleased]: https://github.com/AM-WD/LinkMobility
