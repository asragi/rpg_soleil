# project: rpg_soleil
RPGを作るプロジェクトです．

## 開発環境セットアップ

### Visual Studio のインストール
Visual Studio 2017以降のインストールが必要です．

https://docs.microsoft.com/ja-jp/visualstudio/install/install-visual-studio

また，MonoGameをインストールしてください．

http://www.monogame.net/

3.7.1で動作を確認しています．

### 必要フォントのダウンロード
以下ダウンロードしてインストールしてください．

#### コーポレートロゴ

https://logotype.jp/corporate-logo-font-dl.html

Mediam版をインストールしてください．

### ビルドする
VisualStudioでビルドをします．

### 外部ファイルをビルド先フォルダに移す．
`/Resource/Misc/`内にあるファイルの全てを`soleil/soleil/bin/Windows/x86/Debug/`に移す．

### 動作確認
実行して動いたら完了です．

# Document

## 全体の仕組み
`Game1.cs`の`Update()`が60fpsで実行され，`Draw()`で描画がなされています．全ての大元はここにあると考えて大丈夫です．

ここに`SceneManager`が設置され，Sceneインスタンス上でシーンに応じた様々な処理が記述されています．

## 描画
[ImageBase](/docs/ImageBase.md)
### 画像の描画
- [Image](/docs/Image.md)

### テキストの描画
- [TextImage](/docs/TextImage.md)
  - [RightAlignText](/docs/RightAlignText.md)
  - [TextWithVal](/docs/TextWithVal.md)

## Battle

### ステータス
 - [Person](/docs/Person.md)
   - キャラクターの能力値や装備をまとめるクラス．
 - [AbilityScore](/docs/battle/AbilityScore.md)
   - キャラクターの素の能力値
 - [CharacterStatus](/docs/battle/CharacterStatus.md)
   - 戦闘時に参照される補正込みの能力値
 - [EquipSet](/docs/battle/EquipSet.md)
 - [MagicLv](/docs/battle/MagicLv.md)
