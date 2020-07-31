export function police(prop) {
    if (prop.value) {
        var min = prop.min
        var max = prop.max
        var value = prop.value
        if (prop.propType === 'int' || prop.propType === 'double') {
            min = parseFloat(prop.min)
            max = parseFloat(prop.max)
            value = parseFloat(prop.value)
        }
        if (min) {
            if (value < min) return true
        }
        if (max) {
            if (value > max) return true
        }
    }
}