const { app, BrowserWindow } = require('electron')
const path = require('path')
const url = require('url')

let win

function createWindow() {
    //win = new BrowserWindow({ width: 800, height: 600 })
    win = new BrowserWindow()
    win.loadURL(url.format({
        pathname: path.join(__dirname, 'dist/index.html'),
        protocol: 'file:',
        slashes: true
    }))
    win.webContents.openDevTools()
    win.on('closed', () => {
        win = null
    })
    BrowserWindow.addDevToolsExtension('c:/Users/ivanov.kirill/AppData/Local/Google/Chrome/User Data/Default/Extensions/fmkadmapgofadopljbjfkapdkoienihi/0.15.4_0');
}

app.on('ready', createWindow);
app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        app.quit()
    }
})

app.on('activate', () => {
    if (win === null) {
        createWindow()
    }
})
