class Storage {
    getLocal(key) {
        var value = localStorage.getItem(key);
        if (value)
            return JSON.parse(value);
        else
            return null;
    }

    setLocal(key, value) {
        let _value = JSON.stringify(value);
        localStorage.setItem(key, _value)
    }

    removeLocal(key) {
        localStorage.removeItem(key);
    }

    clearLocal() {
        localStorage.clear();
    }
}
export default new Storage();