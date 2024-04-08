import { FC, PropsWithChildren } from "react"
import './Main.css'

export const Main: FC<PropsWithChildren> = ({ children }) => {
    return <main>
        {children}
    </main>
}