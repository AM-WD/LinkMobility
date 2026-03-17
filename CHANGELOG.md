# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- `Validation` utility class for specifications as MSISDN

### Changed

- Channel implementations (SMS, WhatsApp, ...) are extensions to the `ILinkMobilityClient` interface.
- Reorganized namespaces to reflect parts of the API

### Removed

- `IQueryParameter` as no usage on API docs found (for now)


## [v0.1.1] - 2026-03-13

### Added

- Added optional parameter to inject your own `HttpClient`

### Changed

- Migrated main repository from Gitlab to Gitea


## [v0.1.0] - 2025-12-03

_Initial release, SMS only._

### Added

- `(I)LinkMobilityClient` with client options to communicate with the REST API
- Methods to send SMS messages
  - `SendTextMessage()` sending a normal text message
  - `SendBinaryMessage()` sending a binary message as _base64_ encoded message segments
- `IncomingMessageNotification` to support receiving incoming messages or delivery reports via WebHooks
- UnitTesting



[Unreleased]: https://github.com/AM-WD/LinkMobility/compare/v0.1.1...HEAD

[v0.1.1]: https://github.com/AM-WD/LinkMobility/compare/v0.1.0...v0.1.1
[v0.1.0]: https://github.com/AM-WD/LinkMobility/commits/v0.1.0
