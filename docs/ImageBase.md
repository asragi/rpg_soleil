# ImageBase
画像やテキストの描画に共通する要素をまとめた抽象クラス．画像やテキストのメソッドやフィールドの多くはここに実装されている．

## 使用方法
継承先の[UIImage](UIImage.md)や[FontImage](FontImage.md)のインスタンスを生成して利用する．

イージングアニメーション等のため毎フレーム`Update()`を呼び出す必要がある．

また描画のため`Draw(Drawing)`を呼び出す必要がある．

## Field

### Pos
`Vector`型の位置を表す変数．

### Alpha

### ImageSize{ get; }

### IsStatic
カメラ位置に依存するか否かを設定する変数．

## Method

### Fade(int duration, EaseFunc func, bool isFadeIn)
画像のフェードイン，フェードアウトを実行する．
EaseFuncは`Easing`クラスの関数を指定して渡す．

### MoveTo(Vector target, int duration, EFunc _easeFunc)
指定したポジションにイージングアニメーションで移動します．

### Call()
初期ポジションにイージングアニメーションを伴って出現する．

### Quit()
イージングアニメーションを伴って消失する．
