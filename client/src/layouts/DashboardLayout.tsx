interface Props {
    sidebar: React.ReactNode;
    content: React.ReactNode;
}

export default function DashboardLayout({
    sidebar,
    content
}: Props) {

    return (

        <div
            className="
                h-screen
                overflow-hidden
                bg-gradient-to-br
                from-slate-100
                via-slate-200
                to-blue-100
                p-5
            "
        >

            <div
                className="
                    mx-auto
                    flex
                    h-full
                    max-w-[1800px]
                    overflow-hidden
                    rounded-[28px]
                    border
                    border-white/60
                    bg-white/80
                    shadow-[0_25px_70px_rgba(15,23,42,0.15)]
                    backdrop-blur-xl
                "
            >

                {/* Sidebar */}

                <aside
                    className="
                        w-[360px]
                        flex-shrink-0
                        border-r
                        border-slate-200
                        bg-white
                    "
                >
                    {sidebar}
                </aside>

                {/* Chat */}

                <main
                    className="
                        flex
                        flex-1
                        flex-col
                        bg-slate-50
                    "
                >
                    {content}
                </main>

            </div>

        </div>

    );

}