import { useEffect, useState } from "react"

const useDebounce = <T,>(
    filterFunc: (items: T[], filter: string) => T[],
    items: T[]
) => {
    const [filteredProducts, setFilteredItems] = useState(items)
    const [filter, setFilter] = useState("")
    useEffect(() => {
        const timer = setTimeout(() => {
            setFilteredItems(filterFunc(items, filter))
        }, 600)
        return () => {
            clearTimeout(timer)
        }
    }, [filter, items])
    const onChangeFilter = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFilter(e.target.value)
    }
    return {
        onChangeFilter,
        filter,
        filteredProducts
    }
}

export default useDebounce