# Changelog

All notable changes to this project will be documented in this file. See [conventional commits](https://www.conventionalcommits.org/) for commit guidelines.

## [0.7.0](https://github.com/imbus/robotframework-PlatynUI/compare/v0.6.0..v0.7.0) - 2025-04-11

### Bug Fixes

- **Runtime:** Introduce IDisposable for IDisplayDevice implementations ([33a7a5d](https://github.com/imbus/robotframework-PlatynUI/commit/33a7a5df55c46e91137144728336bdc47da0bc80))
- **macOS:** Correct swift package file ([0538bb0](https://github.com/imbus/robotframework-PlatynUI/commit/0538bb074c4425b15bfc459dbae6761194bd280f))
- **macOS:** Corrected loading of native libraries ([88c8f2b](https://github.com/imbus/robotframework-PlatynUI/commit/88c8f2bc6a0e44fda0a7387eb65da1e9e976e0bf))
- **spy:** Add application title for macox ([80dac35](https://github.com/imbus/robotframework-PlatynUI/commit/80dac35ebce8e7098104189a6bb33f7783f77cbc))
- **testapps:** Correct xamls ([f45ff50](https://github.com/imbus/robotframework-PlatynUI/commit/f45ff50819f51ac60835b382019332acf9b278ad))
- Added outputdir for test results ([6597bd0](https://github.com/imbus/robotframework-PlatynUI/commit/6597bd032bf744e4783ed0c2bec4d74e25d4a981))
- Corrected flag ([0687247](https://github.com/imbus/robotframework-PlatynUI/commit/06872472d2507a7a319e8039290e253b362aa0db))
- Result path now passed as argument to hatch ([85a08fa](https://github.com/imbus/robotframework-PlatynUI/commit/85a08fa5d0efbbe8f5a13445165fdaa1419c3f25))


### Documentation

- Added basic installation process and example templates ([69422bb](https://github.com/imbus/robotframework-PlatynUI/commit/69422bb17ebc0980bea3a738dabf0ded5c3accc8))
- Update README file ([b76df73](https://github.com/imbus/robotframework-PlatynUI/commit/b76df7384644a6923b226d5ab26ed52e2a2e17e1))
- Created a code of conduct ([a689f72](https://github.com/imbus/robotframework-PlatynUI/commit/a689f720f231ea2f4d931de490c63a8ca00bb16c))


### Features

- **MacOS:** Introduce first MacOS support for IDisplayDevice ([320b052](https://github.com/imbus/robotframework-PlatynUI/commit/320b052eeccacd932d5003a13957b9e0c09a7c48))
- **Server:** Implemented first version of PlatynUI.Server CLI ([0cc4848](https://github.com/imbus/robotframework-PlatynUI/commit/0cc48483bb3b885468c4529e27bead3811850ce2))
- **Spy:** Introduce shortcut F6 to highlight the current element ([9a3c69d](https://github.com/imbus/robotframework-PlatynUI/commit/9a3c69d6200a125c6121784d3696b112f6c542fc))
- **runtime:** Support loading .NET extensions from paths defined in PLATYNUI_EXTENSIONS environment variable ([48c13a4](https://github.com/imbus/robotframework-PlatynUI/commit/48c13a4f4102eab0110e4ecd333adf8c269498f6))
- Introduce ElementDescriptor ([b5f431b](https://github.com/imbus/robotframework-PlatynUI/commit/b5f431bcf5f041f3f0be207d5da4a18e8679fb92))
- Make spy compilable on MacOS ([ed47619](https://github.com/imbus/robotframework-PlatynUI/commit/ed476195599031564e149d3646d0aa1e967215c4))
- Add position and button to mouse click keywords ([a6fa01b](https://github.com/imbus/robotframework-PlatynUI/commit/a6fa01b9177ef19646e6aee5e85b748a79059588))
- Implemented `get text` keyword ([39737ba](https://github.com/imbus/robotframework-PlatynUI/commit/39737ba8b882892e58869200f41fa9c135be41fc))
- Added pre-commit hooks ([f303673](https://github.com/imbus/robotframework-PlatynUI/commit/f3036737bd2f56fc3a700626de0f9ca2e38c9f44))
- Added autoformatting, checks and temporary execptions ([4f1b5aa](https://github.com/imbus/robotframework-PlatynUI/commit/4f1b5aa5cf826e142368b516d35437f9d784f1c7))
- Added test execution commands ([777119b](https://github.com/imbus/robotframework-PlatynUI/commit/777119be5a6ae2c498b254313cc6892ceb56b840))


### Testing

- Added matrix configuration test ([fb4447a](https://github.com/imbus/robotframework-PlatynUI/commit/fb4447a8a017045c3b83f8ab26d6e14771f911f9))
- Added matrix configuration test ([45def32](https://github.com/imbus/robotframework-PlatynUI/commit/45def3261d638001f7caca4719a05b2ebbd1cbb0))


## [0.6.0](https://github.com/imbus/robotframework-PlatynUI/compare/v0.5.0..v0.6.0) - 2024-12-13

### Bug Fixes

- **spy:** Enlargement of the search bar ([4bf2280](https://github.com/imbus/robotframework-PlatynUI/commit/4bf2280354522a215d818cb996b2dbe3d4114151))


### Features

- Copy values from the properties view of spy to the clipboard ([287ffa1](https://github.com/imbus/robotframework-PlatynUI/commit/287ffa1df921c0eee836f7dfba79dec523864c2b))
- Changed the way how keywords are implemented ([e13f9ef](https://github.com/imbus/robotframework-PlatynUI/commit/e13f9ef38986d2c4306534552ea797b76ab823e8))


## [0.5.0](https://github.com/imbus/robotframework-PlatynUI/compare/v0.4.1..v0.5.0) - 2024-11-01

### Features

- Support unknown controls for UIAutomation ([ae7095e](https://github.com/imbus/robotframework-PlatynUI/commit/ae7095ec7f60a7085e3b025a9310fce223859d70))


## [0.4.1](https://github.com/imbus/robotframework-PlatynUI/compare/v0.4.0..v0.4.1) - 2024-10-30

### Bug Fixes

- Implement rudimentary control strategy for UIAutomation ([b6f7d50](https://github.com/imbus/robotframework-PlatynUI/commit/b6f7d5015890e217cb35ff8427b3cad132126bdd))


## [0.4.0](https://github.com/imbus/robotframework-PlatynUI/compare/v0.3.1..v0.4.0) - 2024-10-30

### Bug Fixes

- Build platform specific packages ([99cf086](https://github.com/imbus/robotframework-PlatynUI/commit/99cf0868ed53201e57e25f91b22dc0fca113ec2e))
- Add runtime identifer to PlatynUI.Spy to only build the runtimes for the specific platform ([ebcdbb1](https://github.com/imbus/robotframework-PlatynUI/commit/ebcdbb1c211635b50903f65b6dca8205e9772fbb))


### Features

- Build package platform specific ([ae8bb0a](https://github.com/imbus/robotframework-PlatynUI/commit/ae8bb0a33fd8cd667842f477556ee202c7983a88))
- Implement rudimentary window patterns for UIAutomation ([ab5d5d2](https://github.com/imbus/robotframework-PlatynUI/commit/ab5d5d2d54841f45611fb74a4db128381fe1f6b1))


## [0.3.1](https://github.com/imbus/robotframework-PlatynUI/compare/v0.3.0..v0.3.1) - 2024-10-21

### Bug Fixes

- Update gitignore to allow that linux libraries are included in dist build ([3065630](https://github.com/imbus/robotframework-PlatynUI/commit/3065630832a057b362ab58b6ef7efc043ed1b661))


## [0.3.0](https://github.com/imbus/robotframework-PlatynUI/compare/v0.2.0..v0.3.0) - 2024-10-21

### Bug Fixes

- Use os.name instead of platform.platform ([7e962f0](https://github.com/imbus/robotframework-PlatynUI/commit/7e962f0b4f5802e548396c051754de29c15b5b8c))


### Features

- Added first version of the common adapter factory and adapter implementation ([6171b97](https://github.com/imbus/robotframework-PlatynUI/commit/6171b97b8cdafd0cddac47fb75580ee76efcecd8))
- Introduce a provider folder and put the avalonia provider into it ([7375295](https://github.com/imbus/robotframework-PlatynUI/commit/737529544ceaea7389dcdc3f66f6646557889e62))
- Add cli to call PlatynUI.Spy ([4ee9a4d](https://github.com/imbus/robotframework-PlatynUI/commit/4ee9a4d4fe89fe738e9e71619accfde9e35fadd6))
- Make PlatynUI.Spy starting on windows ([9312eb7](https://github.com/imbus/robotframework-PlatynUI/commit/9312eb75535b3b55866f02acbdf2122ebac5a285))


### Refactor

- Fix some imports ([90ec684](https://github.com/imbus/robotframework-PlatynUI/commit/90ec6840562d5dd4f28bbfa2957b46950e296e44))


## [0.2.0](https://github.com/imbus/robotframework-PlatynUI/compare/v0.1.0..v0.2.0) - 2024-10-18

### Bug Fixes

- **Spy:** Some correction theming for different platforms ([5c60486](https://github.com/imbus/robotframework-PlatynUI/commit/5c604867abe107e71c5bdcad8ed1272fea4691a2))
- **Spy:** Some correction for the new logo ([1a20c30](https://github.com/imbus/robotframework-PlatynUI/commit/1a20c306d847eed42abdc27f2317f60ebd21ed69))
- **spy:** Some customizations for linux ([bd86355](https://github.com/imbus/robotframework-PlatynUI/commit/bd863557c0d9fc8dec3766912bcb20762a97f808))
- Corrected flickering of datagrid in spy ([0511704](https://github.com/imbus/robotframework-PlatynUI/commit/05117041bb8ccd37687ef95afb3f2412e85e704c))
- Correct comparing assembly names at loading platform specific parts ([61eaa39](https://github.com/imbus/robotframework-PlatynUI/commit/61eaa398829f7019081b9b2a189de88e4757f43f))
- BoundingRectangle now gives the correct values ([b8ec1a7](https://github.com/imbus/robotframework-PlatynUI/commit/b8ec1a7268e2b883bffb7a55383e8928fe168df5))


### Features

- **Spy:** Be more fluent and define cancel and refresh keybinding ([b9f05c6](https://github.com/imbus/robotframework-PlatynUI/commit/b9f05c6fb4be56bceec3d3b7129c65ab26079ed2))
- **spy:** Add a new window icon ([b39bd9f](https://github.com/imbus/robotframework-PlatynUI/commit/b39bd9f6e346b7a89adfbec870c24b52dcb16c5f))
- Start implementing a platform-independent spy tool ([46d811c](https://github.com/imbus/robotframework-PlatynUI/commit/46d811c6a5cda5590af45e0aa3fde133fef6d33c))
- Search for elements in PlatynUI.Spy using XPath ([051b250](https://github.com/imbus/robotframework-PlatynUI/commit/051b25054e450452ba6b6b3c648855aa446f8ac7))
- Highlighter for Win32 ([d032d9f](https://github.com/imbus/robotframework-PlatynUI/commit/d032d9f272238e93659d16b212c8e04d6068ff67))
- Introduce Semi.Avalonia theme for spy ([f601db7](https://github.com/imbus/robotframework-PlatynUI/commit/f601db7b27344347285b08a8084efdc859fb168b))
- Switch to FluentAvaloniaUI theme ([ae4657a](https://github.com/imbus/robotframework-PlatynUI/commit/ae4657a826ea6459bf163559d5658d153cb2d5b2))
- ApplicationNodes for UIAutomation ([0aa677d](https://github.com/imbus/robotframework-PlatynUI/commit/0aa677d5c85c77c87534c88b535c3318676f75bc))
- Show attributes in spy as a c# expression ([28a743d](https://github.com/imbus/robotframework-PlatynUI/commit/28a743d87f755aa03baad18ecec2be5e865103f4))
- First X11 Desktop support ([3e42cc1](https://github.com/imbus/robotframework-PlatynUI/commit/3e42cc1a651a0338d47732e0d1ffbb79eae86574))
- Simple highlighter for X11 ([42df0ca](https://github.com/imbus/robotframework-PlatynUI/commit/42df0ca5837b8450cb4d1755a155901222b68188))
- Implement some operators for PlatynUI.Runtime.Types ([e204eec](https://github.com/imbus/robotframework-PlatynUI/commit/e204eecfb4e586426f304fba96dc15811c4694cd))
- First version of at-spi2 extension ([6330edc](https://github.com/imbus/robotframework-PlatynUI/commit/6330edc7e5029a72f4b8a2540ce932b68fb14620))
- First version that connects to an avalonia app ([9e3bc45](https://github.com/imbus/robotframework-PlatynUI/commit/9e3bc451c8a752a13709a8be3415bd0c08284e65))
- First version of showing avalonia controls ([18da5c8](https://github.com/imbus/robotframework-PlatynUI/commit/18da5c824db79a58edc4fb83c6d59634602bf5a3))
- Add nativeand clr properties to Avalonia provider ([c091c1b](https://github.com/imbus/robotframework-PlatynUI/commit/c091c1b1697d5b33b8c8355e66820970f33b1a04))
- Optimize highlighter for X11 ([9911f5a](https://github.com/imbus/robotframework-PlatynUI/commit/9911f5a7655f98857a257c934d6a310f558f177e))
- Show some more attributes for at-spi2 ([9b7e37a](https://github.com/imbus/robotframework-PlatynUI/commit/9b7e37acc5c976943998fc8539dad8faf0b088b7))
- Implement IElement for UIAutomationElement ([323c1e4](https://github.com/imbus/robotframework-PlatynUI/commit/323c1e440c8cc44d19c4737941d45c3b34851355))
- Mouse and keyboard device for win32 ([b177496](https://github.com/imbus/robotframework-PlatynUI/commit/b177496453bb7ad9b5e239608f46c0530fc5610f))
- Implement devices for python ([74c003f](https://github.com/imbus/robotframework-PlatynUI/commit/74c003fc830940ce3e3db4e24a7f7cb8a0374842))
- Connect devices to DefaultTechnology ([6b64ad6](https://github.com/imbus/robotframework-PlatynUI/commit/6b64ad68f8c9ca7fff937701c0e85b85d7d5d3b8))


### Refactor

- User own hatch custom hook to build dotnet packages ([fa11f97](https://github.com/imbus/robotframework-PlatynUI/commit/fa11f97c9530b9c8fc2dc1d1291e33c18238be6e))
- Rename some packages ([35d54c4](https://github.com/imbus/robotframework-PlatynUI/commit/35d54c4d3d9f3428d33a9d5decf2d8f9b262c455))


## [0.1.0] - 2024-07-18

### Features

- **Spy:** Add tooltip to search results with full path of the element ([a54d667](https://github.com/imbus/robotframework-PlatynUI/commit/a54d66787dff2efb8351ffbc704eb8db63a2458f))
- **uiautomation:** Implement basic adapters and mouse device ([9e4612d](https://github.com/imbus/robotframework-PlatynUI/commit/9e4612d1ce79cbc34ae1466f38a23aa4fcdadf8e))
- Implement roundtrip for mouse, keyboard, focus, etc. handling ([fa9b235](https://github.com/imbus/robotframework-PlatynUI/commit/fa9b235cb2c5d9533c8cca6f0e8c1811da48ffd4))
- Implement highlighter ([08e01ff](https://github.com/imbus/robotframework-PlatynUI/commit/08e01ff1b1bb63cf206cf8406f01e8ea4a4ffea5))
- Add uiautomation spy/viewer ([2164320](https://github.com/imbus/robotframework-PlatynUI/commit/216432058b3f1dcc910f0ff2748a2ba4d0c04993))
- Add start script for `PlatynUI.UiAutomation.Spy` ([1f060c0](https://github.com/imbus/robotframework-PlatynUI/commit/1f060c071fa1732b0d4efd9052f8d4a953a7ad2d))
- Use different runtimes for coreclr and netfx ([59d1b2b](https://github.com/imbus/robotframework-PlatynUI/commit/59d1b2b18a7173fc4511f50954a527a53beaa3a6))
- Implement keyboard keywords ([0eaad09](https://github.com/imbus/robotframework-PlatynUI/commit/0eaad098133bf294608b852819afbf69356f3f24))
- Implement `ensure_exists` keyword ([b725110](https://github.com/imbus/robotframework-PlatynUI/commit/b725110f201204698e3af7cadbfaae94aeb220e7))


### Refactor

- Optimized enumerating child elements ([8b89143](https://github.com/imbus/robotframework-PlatynUI/commit/8b891437d27215dc7b52d96e1b60bf395f0d11b4))
- Some refactorings in spy ([eb83e89](https://github.com/imbus/robotframework-PlatynUI/commit/eb83e89d2b5e50ec4b4406c715e5650f6f1e3f56))
- Some refactorings ([bf44326](https://github.com/imbus/robotframework-PlatynUI/commit/bf44326da986077ec7ad944c859113ce86a23ce9))
- XPathNavigator reworked and optimized ([9691f7d](https://github.com/imbus/robotframework-PlatynUI/commit/9691f7dde8b9fb55b27499b2965fc5073533a238))


### Testing

- Add wpf test application ([1385331](https://github.com/imbus/robotframework-PlatynUI/commit/1385331260f022a45328cc2f7921d7cf144beab4))
- Simplified WpfTestApp ([df3a2d8](https://github.com/imbus/robotframework-PlatynUI/commit/df3a2d8d9efe347f33f020299b98a405e817553e))


<!-- generated by git-cliff -->
